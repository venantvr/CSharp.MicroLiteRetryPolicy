using System.Collections;
using System.Data.SqlClient;
using System.Reflection;
using System.Runtime.Serialization;

namespace RetryPolicies.Exceptions
{
    public class SqlExceptionHelper
    {
        public static SqlException Generate(SqlExceptionNumber errorNumber)
        {
            return Generate((int) errorNumber);
        }

        public static SqlException Generate(int errorNumber)
        {
            var ex = (SqlException) FormatterServices.GetUninitializedObject(typeof (SqlException));

            var errors = GenerateSqlErrorCollection(errorNumber);
            SetPrivateFieldValue(ex, "_errors", errors);

            return ex;
        }

        private static SqlErrorCollection GenerateSqlErrorCollection(int errorNumber)
        {
            var t = typeof (SqlErrorCollection);

            var col = (SqlErrorCollection) FormatterServices.GetUninitializedObject(t);

            SetPrivateFieldValue(col, "errors", new ArrayList());

            var sqlError = GenerateSqlError(errorNumber);
            var method = t.GetMethod(
                "Add",
                BindingFlags.NonPublic | BindingFlags.Instance
                );
            method.Invoke(col, new object[] { sqlError });

            return col;
        }

        private static SqlError GenerateSqlError(int errorNumber)
        {
            var sqlError = (SqlError) FormatterServices.GetUninitializedObject(typeof (SqlError));

            SetPrivateFieldValue(sqlError, "number", errorNumber);
            SetPrivateFieldValue(sqlError, "message", string.Empty);
            SetPrivateFieldValue(sqlError, "procedure", string.Empty);
            SetPrivateFieldValue(sqlError, "server", string.Empty);
            SetPrivateFieldValue(sqlError, "source", string.Empty);

            return sqlError;
        }

        private static void SetPrivateFieldValue(object obj, string field, object val)
        {
            var member = obj.GetType().GetField(
                field,
                BindingFlags.NonPublic | BindingFlags.Instance
                );
            if (member != null) member.SetValue(obj, val);
        }
    }
}