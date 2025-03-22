using NUnit.Framework;
using PostGhost.IniForUnity;
using UnityEngine;

public abstract class IniTestSuite
{
    private const string IniContents = @"
ColorRed = red
ColorRedHtml = #FF0000FF
ColorRedHtmlNoAlpha = #FF0000

BoolStandardTrue = true
BoolStandardFalse = false

BoolIntTrue = 1
BoolIntFalse = 0

BoolYesTrue = yes
BoolNoFalse = no

BoolOnTrue = on
BoolOffFalse = off
";
    
    protected IniAsset IniAsset { get; private set; }

    [OneTimeSetUp]
    public void Setup()
    {
        IniAsset = IniAsset.Create(IniContents);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        if (IniAsset != null)
        {
            Object.DestroyImmediate(IniAsset);
        }
    }
}