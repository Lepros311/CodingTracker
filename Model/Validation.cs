using System.Globalization;

namespace CodingTracker
{
    internal class Validation
    {
        public static DateTime? ValidateDate(string dateInput)
        {
            string format = "MM/dd/yyyy";
            CultureInfo provider = CultureInfo.InvariantCulture;
            if (!DateTime.TryParseExact(dateInput, format, provider, DateTimeStyles.None, out DateTime date))
            {
                return null;
            }
            else
            {
                return date.Date;
            }
        }

        public static DateTime? ValidateTime(string timeInput)
        {
            string format = "h:mm tt";
            CultureInfo provider = CultureInfo.InvariantCulture;
            if (!DateTime.TryParseExact(timeInput, format, provider, DateTimeStyles.None, out DateTime time))
            {
                return null;
            }
            else
            {
                return time;
            }
        }
    }
}
