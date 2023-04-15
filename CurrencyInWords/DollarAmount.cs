using System.ComponentModel;
using System.Globalization;

namespace CurrencyInWords;

/// <summary>
/// Represents an amount of currency in dollars between 0 and $999 999 999,99.
/// </summary>
public readonly struct DollarAmount
{
    private readonly decimal m_dollars;

    /// <summary>
    /// Full amount in dollars.
    /// Between 0 and 999_999_999.99 (inclusive); Multiple of 0.01 (1 cent).
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Value is negative or greater than 999 999 999.99.</exception>
    /// <exception cref="ArgumentException">Value is not a multiple of 0.01.</exception>
    public decimal Dollars {
        get { return m_dollars; }
        init {
            if (value < 0) {
                throw new ArgumentOutOfRangeException(null, "Value should be positive");
            }
            if (value > 999_999_999.99m) {
                throw new ArgumentOutOfRangeException(null, "Value should be below 999 999 999,99");
            }
            if (value % 0.01m != 0) {
                throw new ArgumentException("Value should be a multiple of 0,01 dollars");
            }
            m_dollars = value;
        }
    }

    /// <summary>
    /// Full amount in cents.
    /// Between 0 and 99_999_999_999 (inclusive).
    /// </summary>
    public long Cents {
        get { return (long) (100 * m_dollars); }
        init { Dollars = 0.01m * value; }
    }

    /// <summary>
    /// Converts a numeric string to a DollarAmount representing the corresponding number of dollars.
    /// </summary>
    /// <param name="s">The string to convert, using "," as a decimal separator.</param>
    /// <returns>The interpretation of <paramref name="s"/> as a number of dollars.</returns>
    /// <exception cref="FormatException"><paramref name="s"/> is not in the correct format.</exception>
    /// <exception cref="OverflowException"><paramref name="s"/> cannot be represented as a decimal.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="s"/> represents a number which is negative or greater than 999 999 999.99.</exception>
    /// <exception cref="ArgumentException"><paramref name="s"/> represents a number which is not a multiple of 0.01.</exception>
    public static DollarAmount Parse(string s) {
        var dollars = Decimal.Parse(
            s,
            NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands,
            new NumberFormatInfo {
                NumberDecimalSeparator = ",",
                NumberGroupSeparator = " "
            }
        );
        return new DollarAmount { Dollars = dollars };
    }

    // Helper: Converts a positive number up to 999 999 999 to english words
    private static string NumberToWords(long num) {
        if (num <= 9) {
            string[] digitWords = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            return digitWords[num];
        } else if (num <= 19) {
            string[] teenWords = { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            return teenWords[num - 10];
        } else if (num <= 99) {
            // First two entries are already covered by previous cases
            string[] tensWords = { "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
            string tens = tensWords[num / 10];
            if (num % 10 == 0) {
                return tens;
            } else {
                // Recurse to use previous cases for one's digit
                return $"{tens}-{NumberToWords(num % 10)}";
            }
        } else if (num <= 999) {
            // Recurse to use previous cases for hundred's digit and rest
            if (num % 100 == 0) {
                return $"{NumberToWords(num / 100)} hundred";
            } else {
                return $"{NumberToWords(num / 100)} hundred {NumberToWords(num % 100)}";
            }
        } else if (num <= 999_999) {
            if (num % 1000 == 0) {
                return $"{NumberToWords(num / 1000)} thousand";
            } else {
                return $"{NumberToWords(num / 1000)} thousand {NumberToWords(num % 1000)}";
            }
        } else if (num <= 999_999_999) {
            if (num % 1000_000 == 0) {
                return $"{NumberToWords(num / 1000_000)} million";
            } else {
                return $"{NumberToWords(num / 1000_000)} million {NumberToWords(num % 1000_000)}";
            }
        } else {
            throw new ArgumentOutOfRangeException(nameof(num), "Argument is larger than 999 999 999; not supported");
        }
    }

    /// <summary>
    /// Converts the DollarAmount to its string representation as English words.
    /// </summary>
    /// <returns>English words describing the amount in dollars.</returns>
    public string ToWords() {
        // Split into dollar and cent parts
        long dollarCount = Cents / 100;
        long centCount = Cents % 100;

        // Convert dollar part into words
        string dollarWords;
        if (dollarCount == 1) {
            dollarWords = "one dollar";
        } else {
            dollarWords = $"{NumberToWords(dollarCount)} dollars";
        }

        // Convert cent part into words
        string centWords;
        if (centCount == 1) {
            centWords = "one cent";
        } else {
            centWords = $"{NumberToWords(centCount)} cents";
        }

        if (centCount == 0) {
            // No cents
            return dollarWords;
        } else {
            // Both dollars and cents
            return $"{dollarWords} and {centWords}";
        }
    }
}
