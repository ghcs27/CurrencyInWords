using CurrencyInWords;

// Read number from console and write converted output back.
string input = Console.ReadLine() ?? "";
string words;
try {
    words = DollarAmount.Parse(input).ToWords();
    Console.WriteLine(words);
} catch (ArgumentException ex) {
    // Value too large or not whole cents
    Console.WriteLine(ex.Message);
} catch (FormatException) {
    Console.WriteLine("Input could not be recognized as a number. Please use ',' (comma) as decimal separator and spaces as optional thousands separator");
} catch (OverflowException) {
    Console.WriteLine("Value is too large or too small to be internally representable");
}
