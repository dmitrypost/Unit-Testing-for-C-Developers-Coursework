using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinjaTests
{
    public class HtmlFormatterTests
    {
        [TestCase("Some content")]
        public void FormatAsBold_GivenString_ReturnsGivenStringWrappedAroundStrong(string content)
        {
            var formatter = new HtmlFormatter();

            var result = formatter.FormatAsBold(content);

            Assert.IsTrue(result.StartsWith("<strong>"));
            Assert.IsTrue(result.EndsWith("</strong>"));
            Assert.IsTrue(result.Contains(content));
        }
    }
}
