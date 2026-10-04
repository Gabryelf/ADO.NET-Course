using System;
using Microsoft.Data.SqlClient;

class Program
{
    static string connectionString =
        "Data Source=(localdb)\\MSSQLLocalDB;" +
        "Initial Catalog=TraineeDB;" +
        "Integrated Security=True;" +
        "TrustServerCertificate=True;";

    static void Main()
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== База студентов ===");
            Console.WriteLine("1. Показать всех студентов");
            Console.WriteLine("2. Найти студента по ID");
            Console.WriteLine("3. Выход");
            Console.Write("Ваш выбор: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ListAllStudents();
                    break;
                case "2":
                    FindStudent();
                    break;
                case "3":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }

            if (running)
            {
                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }
    }

    static void ListAllStudents()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(
                "SELECT StudentId, FirstName, LastName, Age FROM Students", conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine(
                        $"{reader["StudentId"]}: {reader["FirstName"]} " +
                        $"{reader["LastName"]}, {reader["Age"]} лет");
                }
            }
        }
    }

    static void FindStudent()
    {
        Console.Write("Введите ID студента: ");
        string id = Console.ReadLine();

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();

            // Параметризованный запрос — безопасно
            SqlCommand cmd = new SqlCommand(
                "SELECT FirstName, LastName, Age FROM Students WHERE StudentId = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    Console.WriteLine(
                        $"Найден: {reader["FirstName"]} {reader["LastName"]}, " +
                        $"{reader["Age"]} лет");
                }
                else
                {
                    Console.WriteLine("Студент не найден.");
                }
            }
        }
    }
}
