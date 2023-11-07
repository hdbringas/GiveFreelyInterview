using AffiliateProgramManagementSystem.Models;
using AffiliateProgramManagementSystem.Test.Utils;
using FluentValidation.TestHelper;

namespace CustomerProgramManagementSystem.Test.UnitTests;

public class CreateCustomerRequestTests
{
    [Fact]
    public void FailedValidation_NameNull()
    {
        var validator = new CreateCustomerRequestValidator();
        var request = new CreateCustomerRequest()
        {
            Customer = new CustomerDto()
            {
                Name = null
            }
        };
        // Act
        var result = validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Customer.Name);
    }

    [Fact]
    public void FailedValidation_NameEmpty()
    {
        var validator = new CreateCustomerRequestValidator();
        var request = new CreateCustomerRequest()
        {
            Customer = new CustomerDto()
            {
                Name = string.Empty
            }
        };
        // Act
        var result = validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Customer.Name);
    }

    [Fact]
    public void FailedValidation_NameTooLong()
    {
        var validator = new CreateCustomerRequestValidator();
        var request = new CreateCustomerRequest()
        {
            Customer = new CustomerDto()
            {
                Name = TestUtils.RandomString(500)
            }
        };
        // Act
        var result = validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Customer.Name);
    }

    [Fact]
    public void PassedValidation()
    {
        var validator = new CreateCustomerRequestValidator();
        var request = new CreateCustomerRequest()
        {
            Customer = new CustomerDto()
            {
                Name = TestUtils.RandomString(250)
            }
        };
        // Act
        var result = validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Customer.Name);
    }
}
