using Notebook.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.DAL.MSQL
{
    public class MSQLRoleDAO : IRoleDAO
    {
        string _connectionString;

        public void Dispose() { }

        public MSQLRoleDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(string role)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO [Roles] (Title) VALUES (@Title)", connection);
                command.Parameters.Add(new SqlParameter("@Title", role));
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Role> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT [Id], [Title] FROM [Roles]", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return new Role() { Id = (int)reader["Id"], Title = (string)reader["Title"] };
                }
            }
        }

        public bool RoleExists(string role)
        {
            return GetAll().Any(r => r.Title == role);
        }

        public IEnumerable<Role> GetRolesForUser(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT [Role] FROM [UserRole] WHERE [User] = @userId", connection);
                command.Parameters.Add(new SqlParameter("@userId", userId));
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return GetAll().First(r => r.Id == (int)reader["Role"]);
                }
            }
        }

        public int GetId(string role)
        {
            return GetAll().First(r => r.Title == role).Id;
        }

        public bool IsUserInRole(int userId, string role)
        {
            return GetRolesForUser(userId).Any(r => r.Title == role);
        }

        public void AddUserToRole(int userId, string role)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO [UserRole] ([User], [Role]) VALUES (@userId, @roleId)", connection);
                command.Parameters.Add(new SqlParameter("@userId", userId));
                command.Parameters.Add(new SqlParameter("@roleId", GetId(role)));
                connection.Open();
                command.ExecuteNonQuery();
            }                                    
        }
    }
}
