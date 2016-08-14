using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using RetryPolicies.Interfaces;
using MicroLite;

namespace RetryPolicies.Policies
{
    public class MicroLiteRetryPolicy : IRetryPolicy
    {
        private readonly List<Type> _exceptionTypes;
        private readonly int _max;
        private int _count;

        public MicroLiteRetryPolicy()
        {
        }

        public MicroLiteRetryPolicy(int max)
        {
            _max = max;
            _exceptionTypes = new List<Type> { typeof (MicroLiteException) };
        }

        public bool CanRetry(Exception exception)
        {
            var tooManyErrors = _count++ > _max;

            if (tooManyErrors) return false;

            if (_exceptionTypes.Contains(exception.GetType()))
            {
                SqlException sqlException;

                var microLiteException = exception as MicroLiteException;

                if (microLiteException != null)
                {
                    sqlException = microLiteException.InnerException as SqlException;
                }
                else
                {
                    sqlException = exception as SqlException;
                }

                if (sqlException != null && Enum.IsDefined(typeof (SqlExceptionNumber), sqlException.Number))
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        private enum SqlExceptionNumber
        {
            TimeoutExpired = -2,
            EncryptionNotSupported = 20,
            LoginError = 64,
            ConnectionInitialization = 233,
            TransportLevelReceiving = 10053,
            TransportLevelSending = 10054,
            EstablishingConnection = 10060,
            ErrorProcessingRequest = 40197,
            LongRunningTransaction = 40549,
            ProcessingRequest = 40143,
            ServiceBudy = 40501,
            DatabaseOrServerNotAvailable = 40613
        }
    }
}