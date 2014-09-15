using Notebook.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.DAL.MSQL
{
    public class MSQLUserDAO : IUserDAO
    {
        string _connectionString;

        public MSQLUserDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<User> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT [Id], [Name], [Password] FROM [Users]", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return new User() { Id = (int)reader["Id"], Name = (string)reader["Name"], Password = (string)reader["Password"] };
                }
            }
        }

        public void Add(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO [Users] (Name, Password) VALUES (@Name, @Password)", connection);
                command.Parameters.Add(new SqlParameter("@Name", user.Name));
                command.Parameters.Add(new SqlParameter("@Password", user.Password));
                connection.Open();
                command.ExecuteNonQuery();
            }
        }



        public bool UserExists(string name)
        {
            return GetAll().Any(u => u.Name == name);
        }

        public void Dispose()
        {

        }
    }
}
