using System.Runtime.Serialization;

namespace AffiliateProgramManagementSystem.Exceptions;


/// <summary>
/// Throw this exception when input parameters are not valid.
/// </summary>
[Serializable]
public class BadRequestException : ApiException
{
    /// <summary>
    /// Returns 400 Status Code
    /// </summary>
    public override int GetHttpStatusCode()
    {
        return StatusCodes.Status400BadRequest;
    }
    /// <summary>
    /// Constructor
    /// </summary>
    public BadRequestException() : base() { }
    /// <summary>
    /// Constructor
    /// </summary>

    public BadRequestException(string message) : base(message) { }

    /// <summary>
    /// Constructor
    /// </summary>
    public BadRequestException(string message, Exception innerException) : base(message, innerException) { }
    /// <summary>
    /// Constructor
    /// </summary>
    protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    /// <summary>
    /// Serialization
    /// </summary>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
    }
}
