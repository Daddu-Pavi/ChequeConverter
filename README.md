# Cheque Amount Converter

A small ASP.NET Core Razor Pages application that converts a numeric cheque amount into words.

For example:

- Input: `1234.56`
- Output: `One thousand, two hundred and thirty-four dollars and fifty-six cents.`

## Technology

- .NET 9
- ASP.NET Core Razor Pages
- C#
- HTML, CSS and JavaScript
- Bootstrap
- xUnit

## Features

- Converts cheque amounts into English words
- Supports amounts from `0` to `999,999,999.99`
- Supports dollars and cents
- Handles singular and plural wording correctly
- Accepts properly formatted commas, such as `1,234.56`
- Accepts an optional dollar sign, such as `$1,234.56`
- Rejects more than two decimal places
- Rejects negative amounts with a specific validation message
- Displays clear validation messages
- Keeps the entered amount after conversion
- Provides Reset and Copy Words buttons
- Uses a responsive layout for desktop and mobile screens

## How to Run

### Requirements

Install the .NET 9 SDK.

Confirm that it is installed by running:

```powershell
dotnet --version
```

### Run the application

Open a terminal inside the `ChequeConverter.Web` folder and run:

```powershell
dotnet restore
dotnet run
```

Open the localhost address displayed in the terminal, for example:

```text
http://localhost:5088
```

### Run the tests

Open a terminal inside the `ChequeConverter.Tests` folder and run:

```powershell
dotnet test
```

## Testing and Edge Cases

Automated tests are divided into two files:

- `ChequeAmountConverterTests.cs` tests the number-to-words conversion rules.
- `IndexModelTests.cs` tests user-input validation and parsing.

The automated tests cover:

- The example amount `1234.56`
- Zero-dollar amounts
- Cents-only amounts
- Singular dollar and cent wording
- Plural dollar and cent wording
- Teen numbers such as `11` and `19`
- Exact tens such as `40.00`
- Whole hundreds such as `100.00`
- Correct use of “and” in hundreds and thousands
- Thousands and millions
- Skipped number groups such as `1,000,021.00`
- The maximum supported amount
- Negative amounts
- Amounts above the supported maximum
- More than two decimal places
- Empty and whitespace-only input
- Alphabetic input
- Correct and incorrect comma placement
- An optional dollar sign
- Correct hyphenation

The user interface was also checked manually for:

- Conversion
- Reset behaviour
- Copy Words functionality
- Validation messages
- Responsive layout
- Keyboard navigation and button interactions

## Test Results

The project includes 27 automated xUnit test cases covering conversion rules, boundary values, grammar and user-input validation.

Latest result:

- Total: 27
- Passed: 27
- Failed: 0
- Skipped: 0

## Assumptions and Design Decisions

- The application supports amounts from `0` to `999,999,999.99`.
- Amounts with more than two decimal places are rejected rather than rounded.
- Negative amounts are not valid cheque amounts and are rejected.
- English dollar and cent wording is used.
- British/Commonwealth-style wording is used, including “and” where appropriate.
- Correct singular and plural forms are produced, such as `one dollar` and `two dollars`.
- Commas are accepted only when correctly positioned as thousands separators.
- An optional dollar sign is accepted at the beginning of the amount.
- `CultureInfo.InvariantCulture` is used so parsing remains consistent across different server regional settings.
- The amount input uses `type="text"` to support commas and an optional dollar sign.
- The conversion service is stateless and does not use a database, files or external APIs.
- Automated tests focus on conversion rules, validation, boundaries and error handling. The user interface and browser interactions were checked manually.

## Project Structure

```text
ChequeConverter
├── ChequeConverter.sln
├── README.md
├── ChequeConverter.Web
│   ├── Pages
│   │   ├── Index.cshtml              User interface and browser interactions
│   │   ├── Index.cshtml.cs           Server-side validation and page logic
│   │   └── Shared
│   │       └── _Layout.cshtml        Shared page layout
│   ├── Services
│   │   └── ChequeAmountConverter.cs  Number-to-words conversion logic
│   ├── wwwroot
│   │   └── css
│   │       └── site.css              Custom styling and responsive layout
│   └── Program.cs                    Application configuration and startup
└── ChequeConverter.Tests
    ├── ChequeAmountConverterTests.cs Conversion-rule tests
    ├── IndexModelTests.cs            User-input validation tests
    └── ChequeConverter.Tests.csproj  Test project configuration
```