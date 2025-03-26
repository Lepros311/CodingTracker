using Spectre.Console;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                Console.Write("\nEnter the Date (yyyy-MM-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out date))
                {
                    isValidInput = false;
                    Console.WriteLine("Invalid date format. Please enter a date in the format yyyy-MM-dd.");
                }
                else
                {
                    isValidInput = true;
                }
            } while (isValidInput == false);

            return date;
        }

        public static DateTime PromptForStartTime()
        {
            DateTime startTime;
            bool isValidInput = false;
            do
            {
                Console.Write("\nEnter the start time: ");
                if (!DateTime.TryParse(Console.ReadLine(), out startTime))
                {
                    isValidInput = false;
                    Console.WriteLine("Invalid time format. Please enter a time in the format hh:mm.");
                }
                else
                {
                    isValidInput = true;
                }
            } while (isValidInput == false);

            return startTime;
        }

        public static DateTime PromptForEndTime()
        {
            DateTime endTime;
            bool isValidInput = false;
            do
            {
                Console.Write("\nEnter the end time: ");
                if (!DateTime.TryParse(Console.ReadLine(), out endTime))
                {
                    isValidInput = false;
                    Console.WriteLine("Invalid time format. Please enter a time in the format hh:mm.");
                }
                else
                {
                    isValidInput = true;
                }
            } while (isValidInput == false);

            return endTime;
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

        public static DateTime PromptForNewDate(DateTime date)
        {
            Console.Write("\nEnter new date (yyyy-MM-dd) (leave blank to keep current): ");
            string? dateInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(dateInput) && DateTime.TryParse(dateInput, out DateTime newDate))
            {
                date = newDate;
            }

            return date;
        }

        public static DateTime PromptForNewStartTime(DateTime startTime)
        {
            Console.Write("\nEnter new start time (hh:mm) (leave blank to keep current): ");
            string? startTimeInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(startTimeInput) && DateTime.TryParse(startTimeInput, out DateTime newStartTime))
            {
                startTime = newStartTime;
            }

            return startTime;
        }

        public static DateTime PromptForNewEndTime(DateTime endTime)
        {
            Console.Write("\nEnter new end time (hh:mm) (leave blank to keep current): ");
            string? endTimeInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(endTimeInput) && DateTime.TryParse(endTimeInput, out DateTime newEndTime))
            {
                endTime = newEndTime;
            }

            return endTime;
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
