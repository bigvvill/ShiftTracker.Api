using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftTracker.Ui.Services
{
    internal class ShiftEntryServiceUi
    {
        public string GetStartDate()
        {
            Console.WriteLine("\nEnter Start date in yyyy-mm-dd format \n(press Enter to use today as default or 0 to return to Menu):");
            string startDate = Console.ReadLine();

            if (startDate == "0")
            {
                GetUserInput getUserInput = new();
                getUserInput.MainMenu();
            }
            else if (startDate == "")
            {
                startDate = DateOnly.FromDateTime(DateTime.Now).ToString();
                Console.WriteLine(startDate);
            }

            else
            {
                while (!Validation.IsDateValid(startDate))
                {
                    Console.WriteLine("Please enter Start date in the required format: \"yyyy-MM-dd\".");
                    startDate = Console.ReadLine();
                }
            }

            return startDate;
        }

        public string GetEndDate()
        {
            Console.WriteLine("\nEnter End date in yyyy-mm-dd format \n(press Enter to use today as default or 0 to return to Menu):");
            string endDate = Console.ReadLine();

            if (endDate == "0")
            {
                GetUserInput getUserInput = new();
                getUserInput.MainMenu();
            }
            else if (endDate == "")
            {
                endDate = DateOnly.FromDateTime(DateTime.Now).ToString();
                Console.WriteLine(endDate);
            }

            else
            {
                while (!Validation.IsDateValid(endDate))
                {
                    Console.WriteLine("Please enter End date in the required format: \"yyyy-MM-dd\".");
                    endDate = Console.ReadLine();
                }
            }

            return endDate;
        }

        public string GetStartTime()
        {
            Console.WriteLine("\nEnter Start time in hh:mm:ss format \n(press Enter to use current time as default or 0 to return to Menu):");

            string startTime = Console.ReadLine();

            if (startTime == "0")
            {
                GetUserInput getUserInput = new();
                getUserInput.MainMenu();
            }
            else if (startTime == "")
            {
                startTime = TimeOnly.FromDateTime(DateTime.Now).ToString();
                TimeOnly startTimeOnly = TimeOnly.Parse(startTime);
                TimeSpan startTimeSpan = startTimeOnly.ToTimeSpan();
                Console.WriteLine(startTime);
            }

            else
            {
                while (!Validation.IsTimeValid(startTime))
                {
                    Console.WriteLine("Please enter Start time in the required format: \"hh:mm:ss\".");
                    startTime = Console.ReadLine();
                }                
            }

            return startTime;
        }

        public string GetEndTime()
        {
            Console.WriteLine("\nEnter End time in hh:mm:ss format \n(press Enter to use current time as default or 0 to return to Menu):");

            string endTime = Console.ReadLine();

            if (endTime == "0")
            {
                GetUserInput getUserInput = new();
                getUserInput.MainMenu();
            }
            else if (endTime == "")
            {
                endTime = TimeOnly.FromDateTime(DateTime.Now).ToString();
                TimeOnly startTimeOnly = TimeOnly.Parse(endTime);
                TimeSpan startTimeSpan = startTimeOnly.ToTimeSpan();
                Console.WriteLine(endTime);
            }

            else
            {
                while (!Validation.IsTimeValid(endTime))
                {
                    Console.WriteLine("Please enter End time in the required format: \"hh:mm:ss\".");
                    endTime = Console.ReadLine();
                }
            }

            return endTime;
        }

        public decimal GetHourlyRate()
        {
            Console.WriteLine("\nEnter hourly rate in dd.cc format or 0 to return to Menu:");
            string hourlyRate = Console.ReadLine();

            if (hourlyRate == "0")
            {
                GetUserInput getUserInput = new();
                getUserInput.MainMenu();
            }

            while (!Validation.IsMoneyValid(hourlyRate))
            {
                Console.WriteLine("Please enter Hourly Rate in the required format: \"dd.cc\".");
                hourlyRate = Console.ReadLine();
            }

            decimal sqlHourlyRate = decimal.Parse(hourlyRate);

            return sqlHourlyRate;
        }

        internal string GetLocation()
        {
            Console.WriteLine("\nEnter location or 0 to return to Menu:");
            string location = Console.ReadLine();

            if (location == "0")
            {
                GetUserInput getUserInput = new();
                getUserInput.MainMenu();
            }

            while (!Validation.IsStringValid(location))
            {
                Console.WriteLine("Please enter a valid Location.");
                location = Console.ReadLine();
            }

            return location;
        }
    }
}
