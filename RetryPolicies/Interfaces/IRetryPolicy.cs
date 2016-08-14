using System;

namespace RetryPolicies.Interfaces
{
    public interface IRetryPolicy
    {
        bool CanRetry(Exception exception);
    }
}