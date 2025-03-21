﻿using System.Configuration;
using System.Data.SQLite;


namespace CodingTracker
{
    class RecordsController
    {
        bool isValidInput = false;


        public static void AddRecord()
        {
            Console.Clear();

            string? dbPath = ConfigurationManager.AppSettings.Get("dbPath");

            bool isValidInput = false;

            using (var connection = new SQLiteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                DateTime date;
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

                DateTime startTime;
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

                DateTime endTime;
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

                string insertQuery = @"
        INSERT INTO CodingSessions (Date, StartTime, EndTime)
        VALUES (@date, @startTime, @endTime)";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = insertQuery;
                    command.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@startTime", startTime);
                    command.Parameters.AddWithValue("@endTime", endTime);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Display.PrintAllRecords("Add Record");
                        Console.WriteLine("\nRecord added successfully!");
                    }
                    else
                    {
                        Console.WriteLine("\nFailed to add record. Please try again.");
                    }
                }
            }
        }


        public static void EditRecord()
        {
            string? dbPath = ConfigurationManager.AppSettings.Get("dbPath");

            bool isValidInput = false;

            using (var connection = new SQLiteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                Console.Clear();
                Display.PrintAllRecords("Edit Record");

                int recordId = 0;
                do
                {
                    Console.Write("\nEnter the ID of the record you want to edit: ");
                    recordId = GetRecordId(recordId);
                } while (isValidInput == false);

                string selectQuery = @"
        SELECT h.Habit, h.Unit, hi.Date, hi.Quantity
        FROM HabitInstances hi
        JOIN Habits h ON hi.HabitId = h.Id
        WHERE hi.Id = @recordId";

                string? habitName = null; ;
                string? unit = null;
                DateTime? date = null;
                int? quantity = null;

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = selectQuery;
                    command.Parameters.AddWithValue("@recordId", recordId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            habitName = reader.GetString(0);
                            unit = reader.GetString(1);
                            date = reader.GetDateTime(2);
                            quantity = reader.GetInt32(3);
                        }
                    }
                }

                Console.WriteLine($"Selected record ID: {recordId}");
                Console.WriteLine($"Habit: {habitName}");
                Console.WriteLine($"Date: {date:yyyy-MM-dd}");
                Console.WriteLine($"Quantity: {quantity}");
                Console.WriteLine($"Unit: {unit}");

                Console.Write("\nEnter new date (yyyy-MM-dd) (leave blank to keep current): ");
                string? dateInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(dateInput) && DateTime.TryParse(dateInput, out DateTime newDate))
                {
                    date = newDate;
                }

                Console.Write("\nEnter new quantity (leave blank to keep current): ");
                string? quantityInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(quantityInput) && int.TryParse(quantityInput, out int newQuantity))
                {
                    quantity = newQuantity;
                }

                string updateQuery = @"
        UPDATE HabitInstances
        SET Date = @date, Quantity = @quantity
        WHERE Id = @recordId";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = updateQuery;
                    command.Parameters.AddWithValue("@date", date?.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.Parameters.AddWithValue("@recordId", recordId);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Display.PrintAllRecords("Edit Record");
                        Console.WriteLine("\nRecord updated successfully!");
                    }
                    else
                    {
                        Console.WriteLine("\nFailed to update Record. Please try again.");
                    }
                }
            }
        }



        public static void DeleteRecord()
        {
            string? dbPath = ConfigurationManager.AppSettings.Get("dbPath");

            bool isValidInput = false;

            using (var connection = new SQLiteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                Console.Clear();
                Display.PrintAllRecords("Delete Record");

                int recordId = 0;
                do
                {
                    Console.Write("\nEnter the ID of the record you want to delete: ");
                    recordId = GetRecordId(recordId);
                } while (isValidInput == false);

                string? confirmation;
                do
                {
                    Console.Write($"Are you sure you want to delete the record with ID {recordId}? (y/n): ");
                    confirmation = Console.ReadLine();
                    if (confirmation?.ToLower() == "n")
                    {
                        Console.WriteLine("Deletion canceled.");
                        return;
                    }
                    else if (confirmation?.ToLower() != "y")
                    {
                        Console.WriteLine("Invalid response.");
                    }
                } while ((confirmation != "y") && (confirmation != "n"));

                string deleteQuery = @"
        DELETE FROM HabitInstances
        WHERE Id = @recordId";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = deleteQuery;
                    command.Parameters.AddWithValue("@recordId", recordId);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Display.PrintAllRecords("Delete Record");
                        Console.WriteLine("\nRecord deleted successfully!");
                    }
                    else
                    {
                        Console.WriteLine("\nNo record found with that ID. Deletion failed.");
                    }
                }
            }
        }


        public static int GetRecordId(int recordId)
        {
            string? dbPath = ConfigurationManager.AppSettings.Get("dbPath");

            bool isValidInput = false;

            using (var connection = new SQLiteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                if (!int.TryParse(Console.ReadLine(), out recordId))
                {
                    isValidInput = false;
                    Console.WriteLine("Invalid ID. Please enter a numeric value.");
                }
                else
                {
                    string checkHabitInstanceIdQuery = "SELECT COUNT(*) FROM HabitInstances WHERE Id = @recordId";
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = checkHabitInstanceIdQuery;
                        command.Parameters.AddWithValue("@recordId", recordId);

                        int count = Convert.ToInt32(command.ExecuteScalar());
                        if (count > 0)
                        {
                            isValidInput = true;
                        }
                        else
                        {
                            isValidInput = false;
                            Console.WriteLine("Record not found. Please enter a valid record ID.");
                        }
                    }
                }
                return recordId;
            }
        }

    }
}
