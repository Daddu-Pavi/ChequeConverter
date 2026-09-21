using ChequeConverter.Web.Pages;

namespace ChequeConverter.Tests;

public class IndexModelTests
{
    [Fact]
    public void OnPost_AlphabeticInput_ReturnsValidationError()
    {
        // Arrange
        var pageModel = new IndexModel
        {
            Amount = "abc"
        };

        // Act
        pageModel.OnPost();

        // Assert
        Assert.Equal(
            "Enter a valid amount, such as 1,234.56.",
            pageModel.Error);

        Assert.Null(pageModel.Result);
    }
    [Fact]
public void OnPost_IncorrectCommaFormat_ReturnsValidationError()
{
    // Arrange
    var pageModel = new IndexModel
    {
        Amount = "12,34.56"
    };

    // Act
    pageModel.OnPost();

    // Assert
    Assert.Equal(
        "Enter a valid amount, such as 1,234.56.",
        pageModel.Error);

    Assert.Null(pageModel.Result);
}
[Fact]
public void OnPost_CorrectCommaFormat_ReturnsConvertedAmount()
{
    // Arrange
    var pageModel = new IndexModel
    {
        Amount = "1,234.56"
    };

    // Act
    pageModel.OnPost();

    // Assert
    Assert.Null(pageModel.Error);

    Assert.Equal(
        "One thousand, two hundred and thirty-four dollars and fifty-six cents.",
        pageModel.Result);
}
[Theory]
[InlineData("")]
[InlineData("   ")]
public void OnPost_BlankInput_ReturnsRequiredMessage(string input)
{
    // Arrange
    var pageModel = new IndexModel
    {
        Amount = input
    };

    // Act
    pageModel.OnPost();

    // Assert
    Assert.Equal(
        "Please enter a cheque amount.",
        pageModel.Error);

    Assert.Null(pageModel.Result);
}
[Fact]
public void OnPost_AmountWithDollarSign_ReturnsConvertedAmount()
{
    // Arrange
    var pageModel = new IndexModel
    {
        Amount = "$1,234.56"
    };

    // Act
    pageModel.OnPost();

    // Assert
    Assert.Null(pageModel.Error);

    Assert.Equal(
        "One thousand, two hundred and thirty-four dollars and fifty-six cents.",
        pageModel.Result);
}
[Fact]
public void OnPost_MoreThanTwoDecimalPlaces_ReturnsValidationError()
{
    // Arrange
    var pageModel = new IndexModel
    {
        Amount = "123.456"
    };

    // Act
    pageModel.OnPost();

    // Assert
    Assert.Equal(
        "Use no more than two decimal places.",
        pageModel.Error);

    Assert.Null(pageModel.Result);
}
[Theory]
[InlineData("-123.45")]
[InlineData("$-123.45")]
public void OnPost_NegativeAmount_ReturnsSpecificValidationError(string input)
{
    // Arrange
    var pageModel = new IndexModel
    {
        Amount = input
    };

    // Act
    pageModel.OnPost();

    // Assert
    Assert.Equal(
        "Negative amounts are not allowed.",
        pageModel.Error);

    Assert.Null(pageModel.Result);
}
}