namespace AffiliateProgramManagementSystem.Models;

/// <summary>
/// Affiliate DTO
/// </summary>
public record AffiliateDto
{
    /// <summary>
    /// Affiliate Id
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Affiliate Name.
    /// </summary>
    /// <remarks>
    /// Max length 256.
    /// </remarks>
    public string Name { get; set; } = string.Empty;
}
