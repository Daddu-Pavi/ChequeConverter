namespace ChequeConverter.Web.Services;

public class ChequeAmountConverter
{
    private static readonly string[] SmallNumbers =
    {
        "zero", "one", "two", "three", "four",
        "five", "six", "seven", "eight", "nine",
        "ten", "eleven", "twelve", "thirteen", "fourteen",
        "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"
    };
    private static readonly string[] Tens =
{
    "", "", "twenty", "thirty", "forty",
    "fifty", "sixty", "seventy", "eighty", "ninety"
};

    public string ConvertUnderTwenty(int number)
    {
        if (number < 0 || number > 19)
            throw new ArgumentOutOfRangeException(nameof(number));

        return SmallNumbers[number];
    }
    public string ConvertUnderHundred(int number)
{
    if (number < 0 || number > 99)
        throw new ArgumentOutOfRangeException(nameof(number));

    if (number < 20)
        return ConvertUnderTwenty(number);

    int tensDigit = number / 10;
    int onesDigit = number % 10;

    if (onesDigit == 0)
        return Tens[tensDigit];

    return $"{Tens[tensDigit]}-{SmallNumbers[onesDigit]}";
}
public string ConvertUnderThousand(int number)
{
    if (number < 0 || number > 999)
        throw new ArgumentOutOfRangeException(nameof(number));

    if (number < 100)
        return ConvertUnderHundred(number);

    int hundreds = number / 100;
    int remainder = number % 100;

    string words = $"{SmallNumbers[hundreds]} hundred";    

    if (remainder > 0)
        words += $" and {ConvertUnderHundred(remainder)}";

    return words;
}

public string ConvertWholeNumber(long number)
{
    if (number < 0 || number > 999_999_999)
        throw new ArgumentOutOfRangeException(nameof(number));

    if (number == 0)
        return "zero";

    int millions = (int)(number / 1_000_000);
    int thousands = (int)((number / 1_000) % 1_000);
    int units = (int)(number % 1_000);

    var groups = new List<string>();

    if (millions > 0)
        groups.Add($"{ConvertUnderThousand(millions)} million");

    if (thousands > 0)
        groups.Add($"{ConvertUnderThousand(thousands)} thousand");

    if (units > 0)
        groups.Add(ConvertUnderThousand(units));

    if (groups.Count == 1)
        return groups[0];

    int lastNumber = units > 0
        ? units
        : thousands > 0 ? thousands : millions;

    string separator = lastNumber < 100 ? " and " : ", ";

    return string.Join(", ", groups.Take(groups.Count - 1))
           + separator
           + groups[^1];
}
public string ConvertAmount(decimal amount)
{
    if (amount < 0 || amount > 999_999_999.99m)
        throw new ArgumentOutOfRangeException(nameof(amount));

    if (amount * 100m != decimal.Truncate(amount * 100m))
        throw new ArgumentException("Use no more than two decimal places.");

    long dollars = (long)decimal.Truncate(amount);
    int cents = (int)((amount - dollars) * 100m);

    string words;

    if (dollars == 0 && cents > 0)
    {
        string centUnit = cents == 1 ? "cent" : "cents";
        words = $"{ConvertUnderHundred(cents)} {centUnit}";
    }
    else
    {
        string dollarUnit = dollars == 1 ? "dollar" : "dollars";
        words = $"{ConvertWholeNumber(dollars)} {dollarUnit}";

        if (cents > 0)
        {
            string centUnit = cents == 1 ? "cent" : "cents";
            words += $" and {ConvertUnderHundred(cents)} {centUnit}";
        }
    }

    return $"{char.ToUpperInvariant(words[0])}{words[1..]}.";
}
}