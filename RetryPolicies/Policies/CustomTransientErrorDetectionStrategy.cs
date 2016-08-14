using System;
using System.Net;
using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

namespace RetryPolicies.Policies
{
    internal class CustomTransientErrorDetectionStrategy : ITransientErrorDetectionStrategy
    {
        public bool IsTransient(Exception ex)
        {
            if (ex is WebException)
                return true;
            return false;
        }
    }
}