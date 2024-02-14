using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab_7;

public partial class MainWindow : Window
{
    public TextBox SecondsTextBox { get; private set; }
    public TextBox MinutesTextBox { get; private set; }
    public TextBox HoursTextBox { get; private set; }



    void OnClick1(object sender, RoutedEventArgs e)
    {
        Button button = (Button)sender;
        button.Content = "Hakuna matata";

        int seconds, minutes, hours;
        seconds = int.Parse(SecondsTextBox.Text);
        minutes = int.Parse(MinutesTextBox.Text);
        hours = int.Parse(HoursTextBox.Text);
        button.Content = $"{seconds} {minutes} {hours}";

    }


    private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        if (!Char.IsDigit(e.Text, 0))
        {
            e.Handled = true;
        }
    }

    private void TextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        if (!int.TryParse(textBox.Text, out _))
        {
            // Si el texto no es un número entero válido, establecer el valor predeterminado o realizar otra acción
            textBox.Text = "0";
        }
    }




}