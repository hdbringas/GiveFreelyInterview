namespace AffiliateProgramManagementSystem.Models;

/// <summary>
/// Customer DTO
/// </summary>
public record CustomerDto
{
    /// <summary>
    /// Customer Id
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Customer Name.
    /// </summary>
    /// <remarks>
    /// Max length 256.
    /// </remarks>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Customer Name.
    /// </summary>
    /// <remarks>
    /// It must be greater than 0.
    /// </remarks>
    public int AffiliateId { get; set; }
}
