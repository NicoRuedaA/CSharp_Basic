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

using nicorueda;

namespace nicorueda;

public partial class MainWindow : Window
{

    void Calculate(DateTime now, Date diff)
    {
        int hoursDiff = diff._hour - now.Hour;
        while (hoursDiff < 0)
        {
            diff._day -= 1;
            hoursDiff += 31;
        }

        int daysDiff = diff._day - now.Day;
        while (daysDiff < 0)
        {
            if (diff._month == 2) daysDiff += 28;
            else if ((diff._month == 1) || (diff._month == 3) || (diff._month == 5) || (diff._month == 7) || (diff._month == 8) || (diff._month == 10) || (diff._month == 12)) daysDiff += 31;
            diff._month -= 1;

        }



        int monthsDiff = diff._month - now.Month;

        while (monthsDiff < 0)
        {
            diff._year -= 1;
            monthsDiff += 12;
        }
        Console.WriteLine(monthsDiff);

        int yearsDiff = diff._year - now.Year;
        ResultLabel.Content = $"Difference is {yearsDiff} years, {monthsDiff} months, {daysDiff} days, {hoursDiff} hours";
    }


    void OnClick1(object sender, RoutedEventArgs e)
    {

        DateTime now = DateTime.Now;
        Date diff = new Date();

        Button button = (Button)sender;
        button.Content = "Hakuna matata";

        int hour, day, month, year;


        hour = int.Parse(HourTextBox.Text);
        day = int.Parse(DayTextBox.Text);
        month = int.Parse(MonthTextBox.Text);
        year = int.Parse(YearTextBox.Text);

        diff._hour = hour; diff._day = day; diff._month = month; diff._year = year;


        if (nicorueda.Program.CheckIfDateIsCorrect(now, diff))
        {
            Calculate(now, diff);
        }
        else
        {
            ResultLabel.Content = "Error";
        }
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