using System.Globalization;

namespace CodingTracker
{
    class UI
    {
        public static DateTime? PromptForDate()
        {
            DateTime? date = null;
            do
            {
                Console.Write("\nEnter the date (mm/dd/yyyy): ");
                string? dateInput = Console.ReadLine();
                date = Validation.ValidateDate(dateInput!);
                if (date == null)
                {
                    Console.WriteLine("Invalid date format. Please enter a date in the format mm/dd/yyyy.");
                }
            } while (date == null);

            return date;
        }

        public static DateTime? PromptForNewDate(DateTime? date)
        {
            do
            {
                Console.Write("\nEnter new date (mm/dd/yyyy) (leave blank to keep current): ");
                string? dateInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(dateInput))
                {
                    return date;
                }
                else
                {
                    date = Validation.ValidateDate(dateInput);
                    if (date == null)
                    {
                        Console.WriteLine("Invalid date format. Please enter a date in the format mm/dd/yyyy.");
                    }
                    else
                    {
                        date = Validation.ValidateDate(dateInput);
                    }
                }
            } while (date == null);

            return date;
        }

        public static DateTime? PromptForTime(string promptText)
        {
            DateTime? time;
            do
            {
                Console.Write($"\nEnter the {promptText} time (hh:mm am/pm): ");
                string? timeInput = Console.ReadLine();
                time = Validation.ValidateTime(timeInput!);
                if (time == null)
                {
                    Console.WriteLine("Invalid time format. Please enter a time in the format hh:mm am/pm.");
                }
            } while (time == null);

            return time;
        }

        //public static DateTime? PromptForNewTime(DateTime? time, string promptText)
        //{
        //    Console.Write($"\nEnter new {promptText} time (hh:mm am/pm) (leave blank to keep current): ");
        //    string? timeInput = Console.ReadLine();
        //    string format = "hh\\:mm tt";
        //    CultureInfo provider = CultureInfo.InvariantCulture;
        //    if (!string.IsNullOrWhiteSpace(timeInput) && DateTime.TryParseExact(timeInput, format, provider, DateTimeStyles.None, out DateTime newTime))
        //    {
        //        time = newTime;
        //    }

        //    return time;
        //}

        public static DateTime? PromptForNewTime(DateTime? time, string promptText)
        {
            do
            {
                Console.WriteLine($"{time}");
                Console.Write($"\nEnter new {promptText} time (hh:mm am/pm) (leave blank to keep current): ");
                string? timeInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(timeInput))
                {
                    return time;
                }
                else
                {
                    time = Validation.ValidateDate(timeInput);
                    if (time == null)
                    {
                        Console.WriteLine("Invalid date format. Please enter a date in the format mm/dd/yyyy.");
                    }
                    else
                    {
                        time = Validation.ValidateDate(timeInput);
                    }
                }
            } while (time == null);

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
