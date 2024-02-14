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


using static nicorueda.Time;

namespace nicorueda;
public partial class MainWindow : Window
{


    void OnClick1(object sender, RoutedEventArgs e)
    {



        Button button = (Button)sender;
        button.Content = "Hakuna matata";

        int seconds, minutes, hours;
        seconds = int.Parse(SecondsTextBox.Text);
        minutes = int.Parse(MinutesTextBox.Text);
        hours = int.Parse(HoursTextBox.Text);



        Date toTransform = new Date(0, 0, seconds);
        toTransform.Convert();


        ResultLabel.Content = $"{toTransform._hours} {toTransform._minutes} {toTransform._seconds}";
        //howManySeconds(hours, minutes, seconds);
        //nicorueda.Time.howManySeconds
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
            textBox.Text = "0";
        }
    }




}