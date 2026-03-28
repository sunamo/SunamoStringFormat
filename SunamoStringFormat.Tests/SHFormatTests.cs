namespace SunamoStringFormat.Tests;

/// <summary>
/// Tests for <see cref="SHFormat"/> formatting methods.
/// </summary>
public class SHFormatTests
{
    const string leftCurlyBrace = "{";
    const string rightCurlyBrace = "}";

    /// <summary>
    /// Due to { at end, can only be formatted with Format3.
    /// </summary>
    const string formatTemplate = @"export default class {0} extends Component {";
    const string formatExpected = @"export default class a extends Component {";
    const string formatExpectedWildcard = @"export default class *.cs extends Component {";

    const string formatTemplateMultiline = @"export function use{0}() {
	return useGet<{1}>(`{2}`)
}";
    const string formatTemplateMultilineExpected = @"export function usea() {
	return useGet<b>(`c`)
}";

    const string formatTemplateSimple = @"export default class {0}";
    const string formatTemplateSimpleExpected = @"export default class a";

    /// <summary>
    /// Tests Format method which does not allow formatting when there is unfinished {.
    /// </summary>
    [Fact]
    public void FormatTest()
    {
        var actual = SHFormat.Format(formatTemplateSimple, leftCurlyBrace, rightCurlyBrace, "a");
        Assert.Equal(formatTemplateSimpleExpected, actual);
    }

    /// <summary>
    /// Tests Format2 method which does not allow formatting when there is unfinished {.
    /// </summary>
    [Fact]
    public void Format2Test()
    {
        var actual = SHFormat.Format2(formatTemplateSimple, "a");
        Assert.Equal(formatTemplateSimpleExpected, actual);
    }

    /// <summary>
    /// Tests Format3 method which ALLOWS formatting when there is unfinished {.
    /// </summary>
    [Fact]
    public void Format3Test()
    {
        var actual = SHFormat.Format3(formatTemplate, TestData.a);
        Assert.Equal(formatExpected, actual);

        actual = SHFormat.Format3(formatTemplate, TestData.wildcard);
        Assert.Equal(formatExpectedWildcard, actual);

        actual = SHFormat.Format3(formatTemplateMultiline, TestData.listABC.ToArray());
        Assert.Equal(formatTemplateMultilineExpected, actual);
    }

    /// <summary>
    /// Tests Format34 method which attempts both Format4 and Format3.
    /// </summary>
    [Fact]
    public void Format34Test()
    {
        var actual = SHFormat.Format34(formatTemplateMultiline, TestData.listABC);
        Assert.Equal(formatTemplateMultilineExpected, actual);
    }

    /// <summary>
    /// Tests Format4 method which does not allow formatting when there is unfinished {.
    /// </summary>
    [Fact]
    public void Format4Test()
    {
        var actual = SHFormat.Format4(formatTemplateSimple, "a");
        Assert.Equal(formatTemplateSimpleExpected, actual);
    }

    /// <summary>
    /// Tests Format5 method which manually replaces custom-bracketed placeholders.
    /// </summary>
    [Fact]
    public void Format5Test()
    {
        var actual = SHFormat.Format5(formatTemplate, leftCurlyBrace, rightCurlyBrace, TestData.a);
        Assert.Equal(formatExpected, actual);

        actual = SHFormat.Format5(formatTemplate, leftCurlyBrace, rightCurlyBrace, TestData.wildcard);
        Assert.Equal(formatExpectedWildcard, actual);

        actual = SHFormat.Format5(formatTemplateMultiline, leftCurlyBrace, rightCurlyBrace, TestData.listABC.ToArray());
        Assert.Equal(formatTemplateMultilineExpected, actual);
    }
}
