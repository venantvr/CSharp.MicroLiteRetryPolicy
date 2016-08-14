using System;

namespace RetryPolicies.Exceptions
{
    public class MaxCountExpiredException : Exception
    {
        public MaxCountExpiredException(Exception exception)
        {
        }
    }
}