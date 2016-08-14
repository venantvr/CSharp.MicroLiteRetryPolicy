namespace RetryPolicies.Exceptions
{
    public enum SqlExceptionNumber
    {
        TimeoutExpired = -2,
        // Timeout Expired. The timeout period elapsed prior to completion of the operation or the server is not responding
        EncryptionNotSupported = 20,
        // The instance of SQL Server you attempted to connect to does not support encryption
        LoginError = 64,
        // A connection was successfully established with the server, but then an error occurred during the login process
        ConnectionInitialization = 233,
        // The client was unable to establish a connection because of an error during connection initialization process before login
        TransportLevelReceiving = 10053,
        // A transport-level error has occurred when receiving results from the server
        TransportLevelSending = 10054,
        // A transport-level error has occurred when sending the request to the server.
        EstablishingConnection = 10060,
        // A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible
        ErrorProcessingRequest = 40197,
        // The service has encountered an error processing your request. Please try again.
        LongRunningTransaction = 40549,
        // Session is terminated because you have a long-running transaction. Try shortening your transaction.
        ProcessingRequest = 40143,
        // The service has encountered an error processing your request. Please try again.
        ServiceBudy = 40501,
        // The service is currently busy. Retry the request after 10 seconds.
        DatabaseOrServerNotAvailable = 40613
        // Database '%.*ls' on server '%.*ls' is not currently available. Please retry the connection later.
    }
}