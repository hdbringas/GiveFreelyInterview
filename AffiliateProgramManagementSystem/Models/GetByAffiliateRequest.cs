using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AffiliateProgramManagementSystem.Models;

/// <summary>
/// Request model for api/Customers/GetByAffiliate
/// </summary>
public record GetByAffiliateRequest
{
    /// <summary>
    /// Affiliate Id
    /// </summary>
    /// <remarks>
    /// It must be greater than 0.
    /// </remarks>
    [FromQuery]
    public int AffiliateId { set; get; }
}

/// <summary>
/// Validator for GetByAffiliateRequest
/// </summary>
public class GetByAffiliateRequestValidator : AbstractValidator<GetByAffiliateRequest>
{
    public GetByAffiliateRequestValidator()
    {
        RuleFor(x => x.AffiliateId).NotEmpty().InclusiveBetween(0, int.MaxValue);
    }
}