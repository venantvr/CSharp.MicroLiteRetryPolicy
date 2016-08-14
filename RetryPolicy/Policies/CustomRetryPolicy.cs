using System;
using System.Collections.Generic;
using System.Linq;
using RetryPolicies.Interfaces;

namespace RetryPolicies.Policies
{
    public class CustomRetryPolicy : IRetryPolicy
    {
        private readonly List<Type> _exceptionTypes;
        private readonly int _max;
        private int _count;

        public CustomRetryPolicy()
        {
        }

        public CustomRetryPolicy(int max, params Type[] exceptionTypes)
        {
            _max = max;
            _exceptionTypes = exceptionTypes.ToList();
        }

        public bool CanRetry(Exception exception)
        {
            var exceptionResult = _exceptionTypes == null || !_exceptionTypes.Any() || !_exceptionTypes.Contains(exception.GetType());
            var countResult = _count++ > _max;

            if (exceptionResult || countResult)
            {
                return false;
            }

            return true;
        }
    }
}