using NUnit.Framework;
using PostGhost.IniForUnity;
using UnityEngine;

public class ConverterTests : IniTestSuite
{
    [Test]
    [TestCase("ColorRed")]
    [TestCase("ColorRedHtml")]
    [TestCase("ColorRedHtmlNoAlpha")]
    public void Color32_Parses(string key)
    {
        bool didParseValue = IniAsset.RawData.Global.TryGetValue(key, out Color32 parsedValue);
        Assert.IsTrue(didParseValue, "didParseValue");
        Assert.AreEqual(new Color32(255, 0, 0, 255), parsedValue);
    }

    [Test]
    [TestCase("ColorRed")]
    [TestCase("ColorRedHtml")]
    [TestCase("ColorRedHtmlNoAlpha")]
    public void Color_Parses(string key)
    {
        bool didParseValue = IniAsset.RawData.Global.TryGetValue(key, out Color parsedValue);
        Assert.IsTrue(didParseValue, "didParseValue");
        Assert.AreEqual(Color.red, parsedValue);
    }

    [Test]
    [TestCase("BoolStandardTrue", true)]
    [TestCase("BoolIntTrue", true)]
    [TestCase("BoolYesTrue", true)]
    [TestCase("BoolOnTrue", true)]
    [TestCase("BoolStandardFalse", false)]
    [TestCase("BoolIntFalse", false)]
    [TestCase("BoolNoFalse", false)]
    [TestCase("BoolOffFalse", false)]
    public void Bool_Standard_Parses(string trueKey, bool expectedValue)
    {
        bool didParseValue = IniAsset.RawData.Global.TryGetValue(trueKey, out bool parsedValue);
        Assert.IsTrue(didParseValue, "didParseValue");
        Assert.AreEqual(expectedValue, parsedValue);
    }
}