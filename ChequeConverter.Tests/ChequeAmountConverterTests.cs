using ChequeConverter.Web.Services;

namespace ChequeConverter.Tests;

public class ChequeAmountConverterTests
{
    [Fact]
    public void ConvertAmount_ExampleAmount_ReturnsExpectedWords()
    {
        // Arrange
        var converter = new ChequeAmountConverter();
        decimal amount = 1234.56m;

        // Act
        string result = converter.ConvertAmount(amount);

        // Assert
        Assert.Equal(
            "One thousand, two hundred and thirty-four dollars and fifty-six cents.",
            result);
    }

    [Fact]
public void ConvertAmount_Zero_ReturnsZeroDollars()
{
    // Arrange
    var converter = new ChequeAmountConverter();

    // Act
    string result = converter.ConvertAmount(0.00m);

    // Assert
    Assert.Equal("Zero dollars.", result);
}
[Fact]
public void ConvertAmount_OneDollar_UsesSingularDollar()
{
    // Arrange
    var converter = new ChequeAmountConverter();

    // Act
    string result = converter.ConvertAmount(1.00m);

    // Assert
    Assert.Equal("One dollar.", result);
}
[Fact]
public void ConvertAmount_OneCent_UsesSingularCent()
{
    // Arrange
    var converter = new ChequeAmountConverter();

    // Act
    string result = converter.ConvertAmount(0.01m);

    // Assert
    Assert.Equal("One cent.", result);
}
[Fact]
public void ConvertAmount_DollarsAndOneCent_UsesCorrectSingularAndPlural()
{
    // Arrange
    var converter = new ChequeAmountConverter();

    // Act
    string result = converter.ConvertAmount(25.01m);

    // Assert
    Assert.Equal("Twenty-five dollars and one cent.", result);
}
[Fact]
public void ConvertAmount_MaximumAmount_ReturnsExpectedWords()
{
    // Arrange
    var converter = new ChequeAmountConverter();

    // Act
    string result = converter.ConvertAmount(999999999.99m);

    // Assert
    Assert.Equal(
        "Nine hundred and ninety-nine million, " +
        "nine hundred and ninety-nine thousand, " +
        "nine hundred and ninety-nine dollars and ninety-nine cents.",
        result);
}
[Fact]
public void ConvertAmount_NegativeAmount_ThrowsArgumentOutOfRangeException()
{
    // Arrange
    var converter = new ChequeAmountConverter();

    // Act and Assert
    Assert.Throws<ArgumentOutOfRangeException>(
        () => converter.ConvertAmount(-1.00m));
}
[Fact]
public void ConvertAmount_AmountAboveMaximum_ThrowsArgumentOutOfRangeException()
{
    // Arrange
    var converter = new ChequeAmountConverter();

    // Act and Assert
    Assert.Throws<ArgumentOutOfRangeException>(
        () => converter.ConvertAmount(1_000_000_000.00m));
}

[Fact]
public void ConvertAmount_MoreThanTwoDecimalPlaces_ThrowsArgumentException()
{
    // Arrange
    var converter = new ChequeAmountConverter();

    // Act and Assert
    Assert.Throws<ArgumentException>(
        () => converter.ConvertAmount(123.456m));
}
[Fact]
public void ConvertAmount_HundredAndUnits_UsesAndCorrectly()
{
    // Arrange
    var converter = new ChequeAmountConverter();

    // Act
    string result = converter.ConvertAmount(101.00m);

    // Assert
    Assert.Equal("One hundred and one dollars.", result);
}
[Fact]
public void ConvertAmount_ThousandAndUnits_UsesAndCorrectly()
{
    // Arrange
    var converter = new ChequeAmountConverter();

    // Act
    string result = converter.ConvertAmount(1001.00m);

    // Assert
    Assert.Equal("One thousand and one dollars.", result);
}
[Fact]
public void ConvertAmount_CentsOnly_ReturnsCentsWithoutDollars()
{
    // Arrange
    var converter = new ChequeAmountConverter();

    // Act
    string result = converter.ConvertAmount(0.05m);

    // Assert
    Assert.Equal("Five cents.", result);
}
[Fact]
public void ConvertAmount_OneDollarAndOneCent_UsesSingularWords()
{
    // Arrange
    var converter = new ChequeAmountConverter();

    // Act
    string result = converter.ConvertAmount(1.01m);

    // Assert
    Assert.Equal("One dollar and one cent.", result);
}
[Theory]
[InlineData(11, "Eleven dollars.")]
[InlineData(19, "Nineteen dollars.")]
public void ConvertAmount_TeenNumbers_ReturnExpectedWords(
    int amount,
    string expected)
{
    // Arrange
    var converter = new ChequeAmountConverter();

    // Act
    string result = converter.ConvertAmount(amount);

    // Assert
    Assert.Equal(expected, result);
}
[Fact]
public void ConvertAmount_ExactTen_HasNoTrailingHyphen()
{
    // Arrange
    var converter = new ChequeAmountConverter();

    // Act
    string result = converter.ConvertAmount(40.00m);

    // Assert
    Assert.Equal("Forty dollars.", result);
}
[Fact]
public void ConvertAmount_WholeHundred_ReturnsExpectedWords()
{
    // Arrange
    var converter = new ChequeAmountConverter();

    // Act
    string result = converter.ConvertAmount(100.00m);

    // Assert
    Assert.Equal("One hundred dollars.", result);
}
[Fact]
public void ConvertAmount_MillionWithSkippedThousands_ReturnsExpectedWords()
{
    // Arrange
    var converter = new ChequeAmountConverter();

    // Act
    string result = converter.ConvertAmount(1_000_021.00m);

    // Assert
    Assert.Equal(
        "One million and twenty-one dollars.",
        result);
}
}