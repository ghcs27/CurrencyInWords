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
}