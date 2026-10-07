using System;
using DBConnect.Data;
using DBConnect.Models;

namespace DBConnect
{
    class Program
    {
        static string connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;" +
            "Initial Catalog=TraineeDB;" +
            "Integrated Security=True;" +
            "TrustServerCertificate=True;";

        static StudentRepository student_repo = new StudentRepository(connectionString);
        static GroupRepository group_repo = new GroupRepository(connectionString);

        static void Main()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== Добро пожаловать в журнал ===");
                Console.WriteLine("1. Показать всех студентов");
                Console.WriteLine("2. Показать все грууппы");
                Console.WriteLine("3. Добавить");
                Console.WriteLine("4. Редактировать");
                Console.WriteLine("5. Удалить");
                Console.WriteLine("6. Выход");
                Console.Write("Ваш выбор: ");

                switch (Console.ReadLine())
                {
                    case "1": ShowAllStudents(); break;
                    case "2": ShowAllGroups(); break;

                    case "6": running = false; break;
                    default: Console.WriteLine("Неверный выбор."); break;
                }

                if (running)
                {
                    Console.WriteLine("\nНажмите любую клавишу...");
                    Console.ReadKey();
                }
            }
        }

        static void ShowAllStudents()
        {
            var students = student_repo.GetAll();
            if (students.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }

            foreach (var s in students)
                Console.WriteLine(s);
        }

        static void ShowAllGroups()
        {
            var groups = group_repo.GetAllGroup();
            if (groups.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }

            foreach (var g in groups)
                Console.WriteLine(g);
        }
    }
}
