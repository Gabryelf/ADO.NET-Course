using Microsoft.Data.SqlClient;
using System;


namespace AcademyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool is_running = true;
            string conn_str = ConnectDB();
            //AddStudent(conn_str, "Ксения", "Адаменко", "20", "1");

            while (is_running)
            {
                Console.WriteLine(" 1  Просмотреть всех студентов");
                Console.WriteLine(" 2  Найти студента по имени");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllStudents(conn_str);
                        break;
                    case "2":
                        Console.WriteLine(" Введите имя студента");
                        string name = Console.ReadLine();
                        FindStudentByName(conn_str, name);
                        break;
                    case "9":
                        is_running = false;
                        break;
                    default:
                        Console.WriteLine("Введите номер действия!");
                        break;

                }

                if (is_running)
                {
                    Console.WriteLine("Нажмите любую кнопку для продолжения!");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

        }

        static string ConnectDB()
        {
            string conection_string = "Data Source=COMP7A2\\SQLEXPRESS01;" +
                                      "Initial Catalog=Academy;" +
                                      "Integrated Security=True;" +
                                      "TrustServerCertificate=True;";

            return conection_string;
        }

        static void AddGroup(string conn_str, string group_name)
        {
            using (SqlConnection connection = new SqlConnection(conn_str))
            {
                connection.Open();
                string sql_str = "INSERT INTO dbo.Groups(GroupName) VALUES(@name)";

                using (SqlCommand command = new SqlCommand(sql_str, connection))
                {

                    command.Parameters.AddWithValue("@name", group_name);

                    command.ExecuteNonQuery();
                }

            }
        }

        static void AddStudent(string conn_str, string first_name, string last_name, string age, string group_id)
        {
            using (SqlConnection connection = new SqlConnection(conn_str))
            {
                connection.Open();
                string sql_str = "INSERT INTO dbo.Students(FirstName, LastName, Age, GroupId) " +
                                 "VALUES(@firstName, @lastName, @age, @groupId)";

                using (SqlCommand command = new SqlCommand(sql_str, connection))
                {

                    command.Parameters.AddWithValue("@firstName", first_name);
                    command.Parameters.AddWithValue("@lastName", last_name);
                    command.Parameters.AddWithValue("@age", age);
                    command.Parameters.AddWithValue("@groupId", group_id);

                    command.ExecuteNonQuery();

                }

            }
        }

        static void FindStudentByName(string conn_str, string name)
        {
            using (SqlConnection connection = new SqlConnection(conn_str))
            {
                connection.Open();
                string sql_str = "SELECT FirstName, LastName, Age " +
                                 "FROM Students " +
                                 "WHERE FirstName = @name";


                SqlCommand command = new SqlCommand(sql_str, connection);
                command.Parameters.AddWithValue("@name", name);


                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string first_name = reader.GetString(0);
                    string last_name = reader.GetString(1);
                    int age = reader.GetInt32(2);
                    Console.WriteLine($"| {first_name} | {last_name} | {age} |");
                }
            }
        }

        static void ShowAllStudents(string conn_str)
        {
            using (SqlConnection connection = new SqlConnection(conn_str))
            {
                connection.Open();
                string sql_str = "SELECT s.FirstName, s.LastName, s.Age , g.GroupName " +
                                 "FROM Students AS s, Groups AS g " +
                                 "WHERE s.GroupId = g.GroupId";

                using (SqlCommand command = new SqlCommand(sql_str, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string first_name = reader.GetString(0);
                        string last_name = reader.GetString(1);
                        int age = reader.GetInt32(2);
                        string group_name = reader.GetString(3);
                        Console.WriteLine($"| {first_name} | {last_name} | {age} | {group_name} |");
                    }
                }
                
            }
        }
    }
}
