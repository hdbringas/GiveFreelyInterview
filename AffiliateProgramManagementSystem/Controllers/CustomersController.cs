using AffiliateProgramManagementSystem.Exceptions;
using AffiliateProgramManagementSystem.Models;
using AffiliateProgramManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace AffiliateProgramManagementSystem.Controllers;

/// <summary>
/// Controller that contains all the main functions related to Customers
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _service;
    /// <summary>
    /// Constructor
    /// </summary>
    public CustomersController(ICustomerService service)
    {
        _service = service;
    }

    /// <summary>
    /// Creates a new Customer
    /// </summary>
    /// <remarks>
    /// Validates if AffiliateId exists.
    /// No duplicate name validation.
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomerDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiError))]
    public async Task<ActionResult> CreateCustomer(CustomerDto customer)
    {
        var id = await _service.CreateCustomer(customer);

        return CreatedAtAction("GetCustomer", new { customerId = id }, customer with { Id = id });
    }

    /// <summary>
    /// Returns the list of Customers referred by an Affiliate.
    /// </summary>
    [HttpGet("GetByAffiliate")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CustomerDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiError))]
    public async Task<ActionResult> GetByAffiliate([FromQuery] GetByAffiliateRequest request)
    {
        var customers = await _service.GetCustomerByAffiliate(request.AffiliateId);

        return Ok(customers);
    }

    /// <summary>
    /// Returns a Customer filtering by id.
    /// </summary>
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiError))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiError))]
    public async Task<ActionResult> GetCustomer(int customerId)
    {
        var customer = await _service.GetCustomer(customerId);

        return Ok(customer);
    }
}
