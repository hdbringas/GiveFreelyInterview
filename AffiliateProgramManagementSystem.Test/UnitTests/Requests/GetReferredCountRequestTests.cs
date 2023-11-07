using AffiliateProgramManagementSystem.Models;
using FluentValidation.TestHelper;

namespace AffiliateProgramManagementSystem.Test.UnitTests;

public class GetReferredCountRequestTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void FailedValidation(int affiliateId)
    {
        var validator = new GetReferredCountRequestValidator();
        var request = new GetReferredCountRequest()
        {
            AffiliateId = affiliateId
        };
        // Act
        var result = validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.AffiliateId);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(int.MaxValue)]
    public void PassedValidation(int affiliateId)
    {
        var validator = new GetReferredCountRequestValidator();
        var request = new GetReferredCountRequest()
        {
            AffiliateId = affiliateId
        };
        // Act
        var result = validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.AffiliateId);
    }
}
