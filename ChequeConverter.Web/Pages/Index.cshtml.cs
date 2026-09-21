using System.Globalization;
using System.Text.RegularExpressions;
using ChequeConverter.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChequeConverter.Web.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string Amount { get; set; } = string.Empty;

    public string? Result { get; private set; }
    public string? Error { get; private set; }

    public void OnPost()
    {
        string input = Amount?.Trim() ?? string.Empty;

        // Blank input
        if (string.IsNullOrWhiteSpace(input))
        {
            Error = "Please enter a cheque amount.";
            return;
        }

        // Negative input
        if (input.StartsWith('-') || input.StartsWith("$-"))
        {
            Error = "Negative amounts are not allowed.";
            return;
        }

        // More than two decimal places
        int decimalPoint = input.IndexOf('.');

        if (decimalPoint >= 0 &&
            input.Length - decimalPoint - 1 > 2)
        {
            Error = "Use no more than two decimal places.";
            return;
        }

        // Validate the input format
        bool validFormat = Regex.IsMatch(
            input,
            @"^\$?(?:[0-9]+|[0-9]{1,3}(?:,[0-9]{3})+)(?:\.[0-9]{1,2})?$");

        // Convert the input into a decimal value
        if (!validFormat ||
            !decimal.TryParse(
                input.TrimStart('$'),
                NumberStyles.AllowDecimalPoint |
                NumberStyles.AllowThousands,
                CultureInfo.InvariantCulture,
                out decimal amount))
        {
            Error = "Enter a valid amount, such as 1,234.56.";
            return;
        }

        var converter = new ChequeAmountConverter();

        try
        {
            Result = converter.ConvertAmount(amount);
        }
        catch (ArgumentOutOfRangeException)
        {
            Error = "Enter an amount from 0 to 999,999,999.99.";
        }
        catch (ArgumentException)
        {
            Error = "Use no more than two decimal places.";
        }
    }
}