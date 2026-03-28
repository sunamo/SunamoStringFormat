# SunamoStringFormat

Format strings with characters other than `{}` for objects. Part of the Sunamo package ecosystem providing modular, platform-independent utilities for .NET development.

## Features

- **Format** - Uses `string.Format` with error checking, replaces `{}` with custom separators
- **Format2** - Uses `string.Format` with error checking, rejects unfinished `{` without indexed placeholders
- **Format3** - Manually replaces `{x}` placeholders, supports wildcard and multiline templates
- **Format34** - Attempts both `Format4` and `Format3`, suppressing exceptions
- **Format4** - Uses `string.Format` without error checking, supports special patterns like `{0:X2}`
- **Format5** - Manually replaces custom-bracketed placeholders (custom left/right separators)

## Installation

```bash
dotnet add package SunamoStringFormat
```

## Usage

```csharp
using SunamoStringFormat;

// Standard formatting with custom separators
var result = SHFormat.Format("Hello [0]", "[", "]", "World");

// Error-checked formatting
var result2 = SHFormat.Format2("Hello {0}", "World");

// Manual placeholder replacement (works with unfinished braces)
var result3 = SHFormat.Format3("export default class {0} extends Component {", "MyClass");

// Custom separator formatting
var result5 = SHFormat.Format5("Hello <0>", "<", ">", "World");
```

## Target Frameworks

`net10.0`, `net9.0`, `net8.0`

## Links

- [NuGet](https://www.nuget.org/profiles/sunamo)
- [GitHub](https://github.com/sunamo/PlatformIndependentNuGetPackages)
- [Developer site](https://sunamo.cz)

## License

See the repository root for license information.
