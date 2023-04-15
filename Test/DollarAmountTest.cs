using CurrencyInWords;

namespace Test;

[TestClass]
public class DollarAmountTest
{
    [TestMethod]
    public void TestZeroDollars() {
        string input = "0";
        DollarAmount amount = DollarAmount.Parse(input);
        Assert.AreEqual(0m, amount.Dollars);
        string words = amount.ToWords();
        Assert.AreEqual("zero dollars", words);
    }
}