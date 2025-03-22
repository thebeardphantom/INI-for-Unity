using NUnit.Framework;

public class IniAssetTest : IniTestSuite
{
    [Test]
    public void HasRawData()
    {
        Assert.IsNotNull(IniAsset.RawData, "IniAsset.RawData != null");
    }
}