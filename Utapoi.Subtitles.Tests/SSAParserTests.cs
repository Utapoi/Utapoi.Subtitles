// Copyright (c) Utapoi Ltd <contact@utapoi.com>

using NUnit.Framework;
using Utapoi.Subtitles.SSA;

namespace Utapoi.Subtitles.Tests;

[TestFixture]
public class SSAParserTests
{
    [Test]
    public void SforzandoNoZankyoTest()
    {
        var file = SSAConvert.Deserialize("Sforzando no Zankyō.ass");

        Assert.AreEqual(35, file.Lines.Count);
    }
}
