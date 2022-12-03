using ShiftTracker.Ui.Services;
using System.Text.RegularExpressions;

namespace ShiftTracker.Ui
{
    internal class Validation
    {
        internal static bool IsStringValid(string stringInput)
        {
            if (String.IsNullOrEmpty(stringInput) || stringInput.Length > 10)
            {
                return false;
            }

            return true;
        }

        internal static bool IsIdValid(string stringInput)
        {
            foreach (char c in stringInput)
            {
                if (!Char.IsDigit(c))
                    return false;
            }

            if (String.IsNullOrEmpty(stringInput))
            {
                return false;
            }

            return true;
        }

        internal static bool IsDateTimeValid(string stringInput)
        {
            if (String.IsNullOrEmpty(stringInput) || !DateTime.TryParse(stringInput, out _))
            {
                return false;
            }

            return true;
        }

        internal static bool IsEndDateValid(DateTime start, DateTime end)
        {
            bool isValid = start < end ? true : false;

            return isValid;
        }

        internal static bool IsMoneyValid(string pay)
        {
            if (String.IsNullOrEmpty(pay))
            {
                return false;
            }

            Regex rgx = new(@"^[0-9]{0,6}(\.[0-9]{1,2})?$");
            bool isValid = rgx.IsMatch(pay) ? true : false;

            return isValid;
        }

        internal static bool IsShiftIdValid(string shiftId)
        {
            ShiftServiceUi shiftServiceUi = new();

            var shiftIdInt = Int32.Parse(shiftId);
            var shiftToValidate = shiftServiceUi.GetShiftById(shiftIdInt);

            if (shiftToValidate.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }

            else return true;
        }

        internal static bool IsDateValid(string dateInput)
        {
            DateTime fromDateValue;

            var formats = new[] { "yyyy-MM-dd" };

            if (!DateTime.TryParseExact(dateInput, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fromDateValue))
            {
                return false;
            }

            return true;
        }

        internal static bool IsTimeValid(string? timeInput)
        {
            DateTime fromDateValue;

            var formats = new[] { "HH:mm:ss" };

            if (!DateTime.TryParseExact(timeInput, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fromDateValue))
            {
                return false;
            }

            return true;
        }
    }
}
