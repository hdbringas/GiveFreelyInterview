using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AffiliateProgramManagementSystem.Models;

/// <summary>
/// Request model for api/Customers using POST
/// </summary>
public record CreateCustomerRequest
{
    [FromBody]
    public CustomerDto Customer { get; set; } = new();
}

/// <summary>
/// Validator for CreateCustomerRequest
/// </summary>
public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerRequestValidator()
    {
        RuleFor(x => x.Customer.Name).NotNull().NotEmpty().MaximumLength(255);
        RuleFor(x => x.Customer.AffiliateId).NotEmpty().InclusiveBetween(0, int.MaxValue);
    }
}