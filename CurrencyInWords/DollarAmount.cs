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
    public decimal Dollars {
        get { return m_dollars; }
        init {
            if (value < 0) {
                throw new ArgumentOutOfRangeException(
                    $"Value should be positive; got {0.01m * value} dollars"
                );
            }
            if (value > 999_999_999.99m) {
                throw new ArgumentOutOfRangeException(
                    $"Value should be below $999 999 999,99; got {0.01m * value} dollars"
                );
            }
            if (value % 0.01m != 0) {
                throw new ArgumentException(
                    $"Value should be a multiple of 0.01 dollars; got {0.01m * value} dollars"
                );
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
}
