namespace sunamo.Tests.Helpers.Text;
public partial class SHTests
{
    /// <summary>
    /// due to { on end, can be formatted with Format3 only
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
    /// Don't allow format when there is unfinished {
    /// </summary>
    [Fact]
    public void FormatTest()
    {
        // POKUD BYCH ZDE PŘIDÁVAL DALŠÍ NA lsqb / rsqb, PŘIDAT PRO NĚ ŘÁDKY. UŽ TYTO JSEM MĚNIL ZE lsqb na lcub

        // Cant be - { on end
        //var actual = SHFormat.Format(formatTemplate, lcub, rcub, TestData.a);
        //Assert.Equal(formatExpected, actual);

        // Multiline strings is not allowd
        //var actual = SHFormat.Format(formatTemplateMultiline, lcub, rcub, TestData.listABC.ToArray());
        //Assert.Equal(formatTemplateMultilineExpected, actual);

        var actual = SHFormat.Format(formatTemplateSimple, lcub, rcub, "a");
        Assert.Equal(formatTemplateSimpleExpected, actual);
    }

    /// <summary>
    /// Don't allow format when there is unfinished {
    /// </summary>
    [Fact]
    public void Format2Test()
    {
        // POKUD BYCH ZDE PŘIDÁVAL DALŠÍ NA lsqb / rsqb, PŘIDAT PRO NĚ ŘÁDKY. UŽ TYTO JSEM MĚNIL ZE lsqb na lcub

        // Cant be - { on end
        //var actual = SHFormat.Format2(formatTemplate, TestData.a);
        //Assert.Equal(formatExpected, actual);


        // cant be
        //var actual = SHFormat.Format2(formatTemplateMultiline, TestData.listABC);
        //Assert.Equal(formatTemplateMultilineExpected, actual);

        var actual = SHFormat.Format2(formatTemplateSimple, "a");
        Assert.Equal(formatTemplateSimpleExpected, actual);
    }

    /// <summary>
    /// ALLOW format when there is unfinished {
    /// </summary>
    [Fact]
    public void Format3Test()
    {
        // POKUD BYCH ZDE PŘIDÁVAL DALŠÍ NA lsqb / rsqb, PŘIDAT PRO NĚ ŘÁDKY. UŽ TYTO JSEM MĚNIL ZE lsqb na lcub

        var actual = SHFormat.Format3(formatTemplate, TestData.a);
        Assert.Equal(formatExpected, actual);

        actual = SHFormat.Format3(formatTemplate, TestData.wildcard);
        Assert.Equal(formatExpectedWildcard, actual);

        actual = SHFormat.Format3(formatTemplateMultiline, TestData.listABC);
        Assert.Equal(formatTemplateMultilineExpected, actual);
    }

    [Fact]
    public void Format34Test()
    {
        // POKUD BYCH ZDE PŘIDÁVAL DALŠÍ NA lsqb / rsqb, PŘIDAT PRO NĚ ŘÁDKY. UŽ TYTO JSEM MĚNIL ZE lsqb na lcub

        //var actual = SHFormat.Format34(formatTemplate, TestData.a);
        //Assert.Equal(formatExpected, actual);

        //actual = SHFormat.Format34(formatTemplate, TestData.wildcard);
        //Assert.Equal(formatExpectedWildcard, actual);

        var actual = SHFormat.Format34(formatTemplateMultiline, TestData.listABC);
        Assert.Equal(formatTemplateMultilineExpected, actual);
    }

    /// <summary>
    /// Don't allow format when there is unfinished {
    /// </summary>
    [Fact]
    public void Format4Test()
    {
        // POKUD BYCH ZDE PŘIDÁVAL DALŠÍ NA lsqb / rsqb, PŘIDAT PRO NĚ ŘÁDKY. UŽ TYTO JSEM MĚNIL ZE lsqb na lcub

        // Cant be - { on end
        //var actual = SHFormat.Format4(formatTemplate, TestData.a);
        //Assert.Equal(formatExpected, actual);


        // Cant be - multiline
        //actual = SHFormat.Format4(formatTemplateMultiline, lcub, rcub, TestData.listABC);
        //Assert.Equal(formatTemplateMultilineExpected, actual);

        var actual = SHFormat.Format4(formatTemplateSimple, "a");
        Assert.Equal(formatTemplateSimpleExpected, actual);
    }

    [Fact]
    public void Format5Test()
    {
        // POKUD BYCH ZDE PŘIDÁVAL DALŠÍ NA lsqb / rsqb, PŘIDAT PRO NĚ ŘÁDKY. UŽ TYTO JSEM MĚNIL ZE lsqb na lcub

        var actual = SHFormat.Format5(formatTemplate, lcub, rcub, TestData.a);
        Assert.Equal(formatExpected, actual);

        actual = SHFormat.Format5(formatTemplate, lcub, rcub, TestData.wildcard);
        Assert.Equal(formatExpectedWildcard, actual);

        actual = SHFormat.Format5(formatTemplateMultiline, lcub, rcub, TestData.listABC.ToArray());
        Assert.Equal(formatTemplateMultilineExpected, actual);
    }
}
