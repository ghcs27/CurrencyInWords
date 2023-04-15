using CurrencyInWords;

// Read number from console and write converted output back. No error handling.
string input = Console.ReadLine() ?? "";
string words = DollarAmount.Parse(input).ToWords();
Console.WriteLine(words);