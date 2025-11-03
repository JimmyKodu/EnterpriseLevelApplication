using EnterpriseApp.Application.Commands.Products;
using FluentValidation.TestHelper;

namespace EnterpriseApp.UnitTests.Validators;

public class CreateProductCommandValidatorTests
{
    private readonly CreateProductCommandValidator _validator;

    public CreateProductCommandValidatorTests()
    {
        _validator = new CreateProductCommandValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var command = new CreateProductCommand("", "Description", 10.0m, 5, "SKU123", Guid.NewGuid());
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Too_Long()
    {
        var command = new CreateProductCommand(new string('A', 201), "Description", 10.0m, 5, "SKU123", Guid.NewGuid());
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_Have_Error_When_Price_Is_Zero_Or_Negative()
    {
        var command = new CreateProductCommand("Product", "Description", 0m, 5, "SKU123", Guid.NewGuid());
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Price);
    }

    [Fact]
    public void Should_Have_Error_When_StockQuantity_Is_Negative()
    {
        var command = new CreateProductCommand("Product", "Description", 10.0m, -1, "SKU123", Guid.NewGuid());
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.StockQuantity);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new CreateProductCommand("Product", "Description", 10.0m, 5, "SKU123", Guid.NewGuid());
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
