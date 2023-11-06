using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace AffiliateProgramManagementSystem.Exceptions;

/// <summary>
/// Throw this exception if a certain resource is not available or doesn't exist in the given user's context.  
/// We would rather inform a user that a certain resource does not exist if the user has no concept of it, 
/// rather than stating that they do not have access to it.  
/// </summary>
[Serializable]
[ExcludeFromCodeCoverage]
public class NotFoundException : ApiException
{
    /// <summary>
    /// Returns 404 Status Code
    /// </summary>
    public override int GetHttpStatusCode()
    {
        return StatusCodes.Status404NotFound;
    }
    /// <summary>
    /// Constructor
    /// </summary>
    public NotFoundException() : base() { }
    /// <summary>
    /// Constructor
    /// </summary>
    public NotFoundException(string message) : base(message) { }
    /// <summary>
    /// Constructor
    /// </summary>
    public NotFoundException(string message, Exception innerException) : base(message, innerException) { }
    /// <summary>
    /// Constructor
    /// </summary>
    protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    /// <summary>
    /// Serialization
    /// </summary>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
    }
}
