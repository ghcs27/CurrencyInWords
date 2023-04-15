using CurrencyInWords;

namespace Test;

[TestClass]
public class DollarAmountTest
{
    [TestMethod]
    [DataRow("0", 0, "zero dollars")]
    [DataRow("1", 100, "one dollar")]
    [DataRow("25,1", 2510, "twenty-five dollars and ten cents")]
    [DataRow("0,01", 1, "zero dollars and one cent")]
    [DataRow("45 100", 4510000, "forty-five thousand one hundred dollars")]
    [DataRow("999 999 999,99", 999_999_999_99, "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents")]
    public void TestDollarsToWords(string input, long cents, string expectedWords) {
        DollarAmount amount = DollarAmount.Parse(input);
        Assert.AreEqual(cents, amount.Cents);
        string words = amount.ToWords();
        Assert.AreEqual(expectedWords, words);
    }

    [TestMethod]
    [DataRow("xyz")]
    [DataRow("0.5")]
    [DataRow("-1")]
    public void TestFormatException(string input) {
        Assert.ThrowsException<FormatException>(() => {
            DollarAmount.Parse(input);
        });
    }

    [TestMethod]
    [DataRow("100 000 000 000")]
    public void TestRangeException(string input) {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
            DollarAmount.Parse(input);
        });
    }

    [TestMethod]
    [DataRow("0,001")]
    [DataRow("1,234")]
    public void TestArgumentException(string input) {
        Assert.ThrowsException<ArgumentException>(() => {
            DollarAmount.Parse(input);
        });
    }
}