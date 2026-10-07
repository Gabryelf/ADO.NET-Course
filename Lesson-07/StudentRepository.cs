using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using DBConnect.Models;

namespace DBConnect.Data
{
    public class StudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Student> GetAll()
        {
            var result = new List<Student>();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT StudentId, FirstName, LastName, Age FROM Students", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Student
                {
                    StudentId = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Age = reader.GetInt32(3)
                });
            }
            connection.Close();
            return result;
        }
    }
}
