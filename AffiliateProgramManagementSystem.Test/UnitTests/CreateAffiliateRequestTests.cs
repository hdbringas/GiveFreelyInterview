using AffiliateProgramManagementSystem.Models;
using AffiliateProgramManagementSystem.Test.Utils;
using FluentValidation.TestHelper;

namespace AffiliateProgramManagementSystem.Test.UnitTests;

public class CreateAffiliateRequestTests
{
    [Fact]
    public void FailedValidation_NameNull()
    {
        var validator = new CreateAffiliateRequestValidator();
        var request = new CreateAffiliateRequest()
        {
            Affiliate = new AffiliateDto()
            {
                Name = null
            }
        };
        // Act
        var result = validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Affiliate.Name);
    }

    [Fact]
    public void FailedValidation_NameEmpty()
    {
        var validator = new CreateAffiliateRequestValidator();
        var request = new CreateAffiliateRequest()
        {
            Affiliate = new AffiliateDto()
            {
                Name = string.Empty
            }
        };
        // Act
        var result = validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Affiliate.Name);
    }

    [Fact]
    public void FailedValidation_NameTooLong()
    {
        var validator = new CreateAffiliateRequestValidator();
        var request = new CreateAffiliateRequest()
        {
            Affiliate = new AffiliateDto()
            {
                Name = TestUtils.RandomString(500)
            }
        };
        // Act
        var result = validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Affiliate.Name);
    }

    [Fact]
    public void PassedValidation()
    {
        var validator = new CreateAffiliateRequestValidator();
        var request = new CreateAffiliateRequest()
        {
            Affiliate = new AffiliateDto()
            {
                Name = TestUtils.RandomString(250)
            }
        };
        // Act
        var result = validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Affiliate.Name);
    }
}
