using EnterpriseApp.Core.Entities;
using EnterpriseApp.Core.Entities.Base;

namespace EnterpriseApp.UnitTests.Entities;

public class ProductTests
{
    [Fact]
    public void Product_Should_Inherit_From_BaseEntity()
    {
        var product = new Product();
        Assert.IsAssignableFrom<BaseEntity>(product);
    }

    [Fact]
    public void Product_Should_Implement_IAggregateRoot()
    {
        var product = new Product();
        Assert.IsAssignableFrom<IAggregateRoot>(product);
    }

    [Fact]
    public void Product_Should_Initialize_With_Default_Values()
    {
        var product = new Product();
        
        Assert.Equal(string.Empty, product.Name);
        Assert.Equal(string.Empty, product.Description);
        Assert.Equal(0, product.Price);
        Assert.Equal(0, product.StockQuantity);
        Assert.True(product.IsActive);
    }

    [Fact]
    public void Product_Should_Set_Properties_Correctly()
    {
        var categoryId = Guid.NewGuid();
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Test Product",
            Description = "Test Description",
            Price = 99.99m,
            StockQuantity = 100,
            Sku = "TEST-SKU-001",
            CategoryId = categoryId,
            IsActive = true
        };

        Assert.Equal("Test Product", product.Name);
        Assert.Equal("Test Description", product.Description);
        Assert.Equal(99.99m, product.Price);
        Assert.Equal(100, product.StockQuantity);
        Assert.Equal("TEST-SKU-001", product.Sku);
        Assert.Equal(categoryId, product.CategoryId);
        Assert.True(product.IsActive);
    }
}
