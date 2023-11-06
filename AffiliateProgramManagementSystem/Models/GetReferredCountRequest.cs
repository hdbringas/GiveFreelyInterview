using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AffiliateProgramManagementSystem.Models;

/// <summary>
/// Request model for api/Affiliates/ReferredCount endpoint
/// </summary>
public record GetReferredCountRequest
{
    /// <summary>
    /// Affiliate Id.
    /// </summary>
    /// <remarks>
    /// It must be greater than 0.
    /// </remarks>
    [FromQuery]
    public int AffiliateId { get; set; }
}

/// <summary>
/// Validator for GetReferredCountRequest
/// </summary>
public class GetReferredCountRequestValidator : AbstractValidator<GetReferredCountRequest>
{
    public GetReferredCountRequestValidator()
    {
        RuleFor(x => x.AffiliateId)
            .NotEmpty()
            .InclusiveBetween(1, int.MaxValue);
    }
}