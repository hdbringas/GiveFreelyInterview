using AffiliateProgramManagementSystem.Exceptions;
using AffiliateProgramManagementSystem.Models;
using AffiliateProgramManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace AffiliateProgramManagementSystem.Controllers;

/// <summary>
/// Controller that contains all the main functions related to Affiliates
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AffiliatesController : ControllerBase
{
    private readonly IAffiliateService _service;
    /// <summary>
    /// Constructor
    /// </summary>
    public AffiliatesController(IAffiliateService service)
    {
        _service = service;
    }

    /// <summary>
    /// Returns the list of all Affiliates
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AffiliateDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiError))]
    public async Task<ActionResult> GetAffiliates()
    {
        var affiliates = await _service.GetAllAffiliates();

        return Ok(affiliates);
    }

    /// <summary>
    /// Basic commission report returns the count of referred customers by an Affiliate.
    /// </summary>
    [HttpGet("ReferredCount")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiError))]
    public async Task<ActionResult> GetReferredCount([FromQuery] GetReferredCountRequest request)
    {
        var count = await _service.GetReferredCount(request.AffiliateId);

        return Ok(count);
    }

    /// <summary>
    /// Creates a new Affiliate
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AffiliateDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiError))]
    public async Task<ActionResult> CreateAffiliate([FromBody] CreateAffiliateRequest affiliate)
    {
        var id = await _service.CreateAffiliate(affiliate.Affiliate);

        return CreatedAtAction("GetAffiliates", new AffiliateDto() { Id = id, Name = affiliate.Affiliate.Name });
    }
}
