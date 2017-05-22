using System;


namespace Calendar
{
    class Program
    {
        private const int LastDay = 6;

        static void Main(string[] args)
        {
            DateTime date;
            while (true)
            {
                Console.Write("Input date mm dd : ");
                if (DateTime.TryParse(Console.ReadLine(), out date))
                    break;
                Console.WriteLine("Incorrect format!");
            }

            Console.WriteLine("Calendar (on {0}):", date);

            DateTime d = new DateTime();
            for (var i = 0; i < 7; ++i)
            {
                Console.Write("{0:ddd}\t", d);
                d = d.AddDays(1);
                if (i == 4)
                    Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;

            DateTime firstDayInMonth = date.AddDays(-(date.Day - 1));
            int dayPosition = firstDayInMonth.DayOfWeek == DayOfWeek.Sunday ? LastDay : (int)firstDayInMonth.DayOfWeek - 1; // conversion DayOfWeek(Enum) to int given that the start of the week = Monday (not Sunday)            
            int workingDays = 0;
            var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);

            for (int i = 0; i < dayPosition; i++)
                Console.Write("\t");

            for (int day = 1; day <= daysInMonth; day++)
            {
                if (dayPosition % 7 == 6 || dayPosition % 7 == 5)
                    Console.ForegroundColor = ConsoleColor.Red;

                if (day == date.Day)
                    Console.ForegroundColor = ConsoleColor.Blue;

                if (day == DateTime.Today.Day &&
                    DateTime.Today.Month == date.Month &&
                    DateTime.Today.Year == date.Year)
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.Write(day);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;

                if (dayPosition != LastDay - 1 && dayPosition != LastDay)
                    workingDays++;
                
                bool writeNewLine = false;
                dayPosition++;
                if (dayPosition == LastDay + 1 || day == daysInMonth)
                {
                    dayPosition = 0;
                    writeNewLine = true;
                }
                if (writeNewLine)            
                    Console.WriteLine();      
                else              
                    Console.Write("\t");
            }
            Console.WriteLine("There are {0} working days in this month.", workingDays);
            Console.ReadKey();
        }
    }
}