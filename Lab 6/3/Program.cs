namespace nicorueda
{
    public static class Time
    {
        public static int hoursToMinutes(int h)
        {
            return h * 60;
        }


        public static int minutesToSeconds(int m)
        {
            return m * 60;
        }


        public static int minutesToHours(int h, ref int res)
        {
            res = h % 60;
            return h / 60;
        }


        public static int howManySeconds(int h = 0, int m = 0, int s = 0)
        {
            int hoursInSeconds = hoursToMinutes(h);
            int minutesInSconds = minutesToSeconds(m + hoursInSeconds);
            return s + minutesInSconds;
        }


        public static int secondsToMinutes(int s, ref int res)
        {
            res = s % 60;
            return s / 60;
        }
    }


    public class Date
    {
        public int _year;
        public int _month;
        public int _day;
        public int _hour;
        public int _minute;
        public int _second;


        public Date(int h = 0, int m = 0, int s = 0)
        {
            _hour = h; _minute = m; _second = s;
        }


        public void Convert()
        {
            if (_second / 60 >= 1)
            {
                int res = 0;
                int newMinutes = Time.secondsToMinutes(_second, ref res);
                _second = res;
                _minute += newMinutes;
            }

            if (_minute / 60 > 1)
            {
                int res = 0;
                int newHours = Time.minutesToHours(_minute, ref res);
                _minute = res;
                _hour += newHours;
            }
        }


        public void Print()
        {
            Console.WriteLine($"This is equal to {_hour} hours, {_minute} minutes and {_second} seconds");
        }
    }




    static class Program
    {
        static int min = 0;
        static int max = 99999999;


        static bool CheckIfDateIsCorrect(DateTime now, Date diff)
        {
            if ((diff._day == 31) && !((diff._month == 1) || (diff._month == 3) || (diff._month == 5) || (diff._month == 7) || (diff._month == 8) || (diff._month == 10) || (diff._month == 12)))
            {
                return false;
            }
            else if ((diff._day >= 29) && (diff._month == 2))
            {
                return false;
            }



            if (now.Year > diff._year) return false;
            else if (now.Year == diff._year)
            {
                if (now.Month > diff._month) return false;
                else if (now.Month == diff._month)
                {
                    if (now.Day > diff._day) return false;
                    else if (now.Day == diff._day)
                    {
                        if (now.Hour > diff._hour) return false;
                        else return true;
                    }
                    else return true;
                }
                else return true;
            }
            else return true;
        }


        static void CalculateDiff(DateTime now, Date diff)
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

            Console.WriteLine(daysDiff);

            int monthsDiff = diff._month - now.Month;

            while (monthsDiff < 0)
            {
                diff._year -= 1;
                monthsDiff += 12;
            }
            Console.WriteLine(monthsDiff);

            int yearsDiff = diff._year - now.Year;
            Console.WriteLine(yearsDiff);
            Console.WriteLine($"Difference is {yearsDiff} years, {monthsDiff} months, {daysDiff} days, {hoursDiff} hours");
        }


        static void Main()
        {
            DateTime now = DateTime.Now;
            Date diff = new Date();

            do
            {
                int hour = Input.readInt("Enter the hour: ", min, 24);
                int day = Input.readInt("Enter the day: ", min, 31);
                int month = Input.readInt("Enter the month: ", min, 12);
                int year = Input.readInt("Enter the year: ", min, max);

                diff._hour = hour; diff._day = day; diff._month = month; diff._year = year;

            } while (!CheckIfDateIsCorrect(now, diff));

            CalculateDiff(now, diff);
        }
    }
}
