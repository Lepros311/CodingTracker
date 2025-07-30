using System.Globalization;

namespace CodingTracker
{
    internal class Validation
    {
        public DateTime? ValidateDate(string dateInput)
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

        public static bool ValidateStartTimeIsLessThanEndTime(DateTime? start, DateTime? end)
        {
            if (start < end)
                return true;
            else
                return false;
        }

        public static (string message, bool validStatus, int recordId) ValidateRecordId(string? recordIdInput)
        {
            if (!int.TryParse(recordIdInput, out int recordId))
                return ("Invalid ID. Please enter a numeric value.", false, recordId);
            else if (RecordsController.GetRecordIdCount(recordId) <= 0)
                return ("Record not found. Please enter a valid record ID.", false, recordId);
            else
                return ("", true, recordId);
        }

        public static string ValidateDeleteConfirmation(string? confirmationInput)
        {
            if (confirmationInput?.ToLower() == "n")
            {
                return "n";
            }
            else if (confirmationInput?.ToLower() != "y")
            {
                return "";
            }

            return "y";
        }
    }
}
