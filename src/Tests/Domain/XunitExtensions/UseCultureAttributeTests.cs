// Copyright 2014 Outercurve Foundation
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace InvoiceKit.Tests.Domain.XunitExtensions;

using System.Reflection.Emit;
using Xunit.Sdk;

public class UseCultureAttributeTests
{
    [Theory]
    [InlineData("en-US")]
    [InlineData("da-DK")]
    [InlineData("de-DE")]
    public void CreatingWithCultureSetsCorrectCultureProperty(string culture)
    {
        var attr = new UseCultureAttribute(culture);

        Assert.Equal(culture, attr.Culture.Name);
    }

    [Theory]
    [InlineData("nl-BE")]
    [InlineData("fi-FI")]
    [InlineData("fr-CA")]
    public void CreatingWithCultureAndUICultureSetsCorrectCulturePropery(string culture)
    {
        var attr = new UseCultureAttribute(culture, "fr");

        Assert.Equal(culture, attr.Culture.Name);
    }

    [Theory]
    [InlineData("fr-FR")]
    [InlineData("es-ES")]
    [InlineData("zh-HK")]
    public void CreatingWithCultureSetsSameUICulture(string culture)
    {
        var attr = new UseCultureAttribute(culture);

        Assert.Equal(culture, attr.UICulture.Name);
    }

    [Theory]
    [InlineData("nl-NL")]
    [InlineData("de-AT")]
    [InlineData("en-GB")]
    public void CreatingWithCultureAndUICultureSetsCorrectUICulturePropery(string uiCulture)
    {
        var attr = new UseCultureAttribute("el-GR", uiCulture);

        Assert.Equal(uiCulture, attr.UICulture.Name);
    }

    [Fact]
    public void IsBeforeAfterAttribute()
    {
        Assert.IsAssignableFrom<BeforeAfterTestAttribute>(new UseCultureAttribute("ga-IE"));
    }

    [Theory]
    [InlineData("it-IT")]
    [InlineData("ja-JP")]
    [InlineData("nb-NO")]
    public void CultureIsChangedWithinTest(string culture)
    {
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        var attr = new UseCultureAttribute(culture);

        attr.Before(new DynamicMethod("test", typeof(void), [], typeof(UseCultureAttributeTests)));

        Assert.Equal(attr.Culture, Thread.CurrentThread.CurrentCulture);

        attr.After(new DynamicMethod("test", typeof(void), [], typeof(UseCultureAttributeTests)));

        Assert.Equal(originalCulture, Thread.CurrentThread.CurrentCulture);
    }

    [Theory]
    [InlineData("pt-BR")]
    [InlineData("pa-IN")]
    [InlineData("rm-CH")]
    public void UICultureIsChangedWithinTest(string uiCulture)
    {
        var originalUICulture = Thread.CurrentThread.CurrentUICulture;
        var attr = new UseCultureAttribute("ru-RU", uiCulture);

        attr.Before(new DynamicMethod("test", typeof(void), [], typeof(UseCultureAttributeTests)));

        Assert.Equal(attr.UICulture, Thread.CurrentThread.CurrentUICulture);

        attr.After(new DynamicMethod("test", typeof(void), [], typeof(UseCultureAttributeTests)));

        Assert.Equal(originalUICulture, Thread.CurrentThread.CurrentUICulture);
    }

    [Fact, UseCulture("sv-SE")]
    public void AttributeChangesCultureToSwedishInTestMethod()
    {
        Assert.Equal("sv-SE", Thread.CurrentThread.CurrentCulture.Name);
    }

    [Fact, UseCulture("th-TH", "es-CL")]
    public void AttributeChangesUICultureToChileanSpanishInTestMethod()
    {
        Assert.Equal("es-CL", Thread.CurrentThread.CurrentUICulture.Name);
    }
}
