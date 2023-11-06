using System.Runtime.Serialization;

namespace AffiliateProgramManagementSystem.Exceptions;

/// <summary>
/// An abstract base class that all API exceptions should inherit from.
/// </summary>
/// <remarks>
/// In order to make defensive programming easier, we want to throw validation and other errors from internal code that,
/// if not caught and handled before the API sends a response to the client, will provide a correct HTTP status code, a tracing mechanism
/// and a useful error message.  In order to do this, all exceptions that could wind up being thrown by the API should
/// inherit from this class and have their correct status codes used. 
/// By convention, we want to name the exception with a matching HTTP Status Code name as found in the c# .net 7 StatusCodes enum.
/// </remarks>
[Serializable]
public abstract class ApiException : Exception
{
    /// <summary>
    /// This object is returned to the client in the response body.  It should not contain sensitive information.
    /// </summary>
    public dynamic? ReturnData { get; set; }

    /// <summary>
    /// Get Status code
    /// </summary>
    /// <returns></returns>
    public abstract int GetHttpStatusCode();
    
    /// <summary>
    /// Constructor
    /// </summary>
    protected ApiException() : base() { }
    /// <summary>
    /// Constructor
    /// </summary>
    protected ApiException(string message) : base(message) { }
    /// <summary>
    /// Constructor
    /// </summary>
    protected ApiException(string message, Exception innerException) : base(message, innerException) { }
    /// <summary>
    /// Constructor
    /// </summary>
    protected ApiException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }
    /// <summary>
    /// Serialization
    /// </summary>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
    }
}
