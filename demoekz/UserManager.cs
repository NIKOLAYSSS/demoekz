using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoekz
{
    public class UserManager
    {
        private readonly string _connectionString;

        public UserManager(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void RegisterUser(string username, string passwordHash)
        {
            string query = "INSERT INTO users (username, password_hash) VALUES (@username, @passwordHash)";
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("username", username);
                    command.Parameters.AddWithValue("passwordHash", passwordHash);
                    command.Parameters.AddWithValue("role", "User");
                    command.ExecuteNonQuery();
                }
            }
        }

        // Метод для получения хэша пароля по имени пользователя
        public string GetPasswordHash(string username)
        {
            string query = "SELECT password_hash FROM users WHERE username = @username";
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("username", username);
                    var result = command.ExecuteScalar();
                    return result?.ToString(); // Возвращаем строку или null, если пользователь не найден
                }
            }
        }

        public string GetUserRole(string username)
        {
            string query = "SELECT role FROM users WHERE username = @username";
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("username", username);
                    var result = command.ExecuteScalar();
                    return result?.ToString(); // Возвращаем роль или null, если пользователь не найден
                }
            }
        }
    }
}
