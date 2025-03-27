using System.Globalization;

namespace CodingTracker
{
    class UI
    {
        public static DateTime PromptForDate()
        {
            DateTime date;
            bool isValidInput = false;
            do
            {
                Console.Write("\nEnter the date (mm/dd/yyyy): ");
                string? dateInput = Console.ReadLine();
                //isValidInput = Validation.IsValidDate(dateInput);
                string format = "MM/dd/yyyy";
                CultureInfo provider = CultureInfo.InvariantCulture;
                if (!DateTime.TryParseExact(dateInput, format, provider, DateTimeStyles.None, out date))
                {
                    isValidInput = false;
                    Console.WriteLine("Invalid date format. Please enter a date in the format mm/dd/yyyy.");
                }
                else
                {
                    isValidInput = true;
                }
            } while (isValidInput == false);

            return date;
        }

        public static DateTime PromptForNewDate(DateTime date)
        {
            Console.Write("\nEnter new date (mm/dd/yyyy) (leave blank to keep current): ");
            string? dateInput = Console.ReadLine();
            string format = "MM/dd/yyyy";
            CultureInfo provider = CultureInfo.InvariantCulture;
            if (!string.IsNullOrWhiteSpace(dateInput) && DateTime.TryParseExact(dateInput, format, provider, DateTimeStyles.None, out DateTime newDate))
            {
                date = newDate;
            }

            return date;
        }

        public static DateTime PromptForTime(string promptText)
        {
            DateTime time;
            bool isValidInput = false;
            do
            {
                Console.Write($"\nEnter the {promptText} time (hh:mm am/pm): ");
                string format = "hh\\:mm tt";
                CultureInfo provider = CultureInfo.InvariantCulture;
                if (!DateTime.TryParseExact(Console.ReadLine(), format, provider, DateTimeStyles.None, out time))
                {
                    isValidInput = false;
                    Console.WriteLine("Invalid time format. Please enter a time in the format hh:mm.");
                }
                else
                {
                    isValidInput = true;
                }
            } while (isValidInput == false);

            return time;
        }

        public static DateTime? PromptForNewTime(DateTime? time, string promptText)
        {
            Console.Write($"\nEnter new {promptText} time (hh:mm am/pm) (leave blank to keep current): ");
            string? timeInput = Console.ReadLine();
            string format = "hh\\:mm tt";
            CultureInfo provider = CultureInfo.InvariantCulture;
            if (!string.IsNullOrWhiteSpace(timeInput) && DateTime.TryParseExact(timeInput, format, provider, DateTimeStyles.None, out DateTime newTime))
            {
                time = newTime;
            }

            return time;
        }

        public static int PromptForRecordId(string promptText)
        {
            int recordId = 0;
            do
            {
                Console.Write($"\nEnter the ID of the record you want to {promptText}: ");
                recordId = RecordsController.GetRecordId(recordId);
            } while (recordId <= 0);

            return recordId;
        }

        public static string PromptForDeleteConfirmation(int recordId)
        {
            string? confirmation;
            do
            {
                Console.Write($"Are you sure you want to delete the record with ID {recordId}? (y/n): ");
                confirmation = Console.ReadLine();
                if (confirmation?.ToLower() == "n")
                {
                    Console.WriteLine("Deletion canceled.");
                    return "n";
                }
                else if (confirmation?.ToLower() != "y")
                {
                    Console.WriteLine("Invalid response.");
                }
            } while ((confirmation != "y") && (confirmation != "n"));

            return "y";
        }
    }
}
