using System;
using System.Windows;
using CurrencyInWords;

namespace CurrencyInWordsGUI;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow() {
        InitializeComponent();
    }

    private void ConvertBtn_Click(object sender, RoutedEventArgs e) {
        string input = InputTxt.Text;
        string output;
        try {
            output = DollarAmount.Parse(input).ToWords();
        } catch (ArgumentException ex) {
            output = ex.Message;
        } catch (FormatException) {
            output = "Input could not be recognized as a number. Please use ',' (comma) as decimal separator and spaces as optional thousands separator";
        } catch (OverflowException) {
            output = "Value is too large or too small to be internally representable";
        }
        OutputTxt.Text = output;
    }
}
