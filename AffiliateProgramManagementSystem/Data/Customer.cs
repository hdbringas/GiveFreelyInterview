using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AffiliateProgramManagementSystem.Data;

/// <summary>
/// Customer Entity
/// </summary>
public class Customer
{
    /// <summary>
    /// Customer Id
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Customer Name 
    /// </summary>
    /// <remarks>
    /// Max length 256.
    /// </remarks>
    [Required]
    [StringLength(255)]
    public required string Name { get; set; }

    /// <summary>
    /// AffiliateId
    /// </summary>
    [ForeignKey("Affiliate")]
    public int AffiliateID { get; set; }

    /// <summary>
    /// Navigation property for Affiliate 
    /// </summary>
    public Affiliate? Affiliate
    {
        get; set;
    }
}