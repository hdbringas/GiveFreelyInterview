using System.ComponentModel.DataAnnotations;

namespace AffiliateProgramManagementSystem.Data;

/// <summary>
/// Affiliate Entity
/// </summary>
public class Affiliate
{
    private ICollection<Customer>? _customers;

    /// <summary>
    /// Affiliate Id
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Affiliate Name.
    /// </summary>
    /// <remarks>
    /// Max length 256.
    /// </remarks>
    [Required]
    [StringLength(255)]
    public required string Name { get; set; }

    /// <summary>
    /// Navigation property for Customers
    /// </summary>
    public ICollection<Customer> Customers => _customers ??= new List<Customer>();
}
