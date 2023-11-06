using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AffiliateProgramManagementSystem.Models;

/// <summary>
/// Request model for api/Affiliates using POST
/// </summary>
public record CreateAffiliateRequest
{
    /// <summary>
    /// New Affiliate information.
    /// </summary>
    [FromBody]
    public AffiliateDto Affiliate { get; set; } = new();
}

/// <summary>
/// Validator for CreateAffiliateRequest
/// </summary>
public class CreateAffiliateRequestValidator : AbstractValidator<CreateAffiliateRequest>
{
    public CreateAffiliateRequestValidator()
    {
        RuleFor(x => x.Affiliate.Name).NotNull().NotEmpty().MaximumLength(255);
    }
}