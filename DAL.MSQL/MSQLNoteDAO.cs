using Notebook.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace Notebook.DAL.MSQL
{
    public class MSQLNoteDAO : INoteDAO
    {
        string _connectionString;

        public void Dispose() { }

        public MSQLNoteDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Note> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT [Id], [Author], [CreationDate], [Content] FROM [Notes]", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return new Note() { 
                        Id = (int)reader["Id"], 
                        Author = (int)reader["Author"], 
                        CreationDate = (DateTime)reader["CreationDate"],
                        Content = (string)reader["Content"]
                    };
                }
            }            
        }

        public void Add(Note note)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO [Notes] (Author, CreationDate, Content) VALUES (@Author, @CreationDate, @Content)", connection);
                command.Parameters.Add(new SqlParameter("@Author", note.Author));
                command.Parameters.Add(new SqlParameter("@CreationDate", note.CreationDate));
                command.Parameters.Add(new SqlParameter("@Content", note.Content));
                connection.Open();
                command.ExecuteNonQuery();
            }                        
        }

        public void Update(int id, string content)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE [Notes] SET Content=@content WHERE [Id] = @id", connection);
                command.Parameters.Add(new SqlParameter("@content", content));
                command.Parameters.Add(new SqlParameter("@id", id));
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM [Notes] WHERE [Id] = @Id", connection);
                command.Parameters.Add(new SqlParameter("@Id", id));
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Dislike(int userId, int noteId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE [Likes] WHERE [User] = @userId AND [Note] = @noteId", connection);
                command.Parameters.Add(new SqlParameter("@userId", userId));
                command.Parameters.Add(new SqlParameter("@noteId", noteId));
                connection.Open();
                command.ExecuteNonQuery();
            }                        

        }

        public void Like(int userId, int noteId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO [Likes] ([User], [Note]) VALUES (@userId, @noteId)", connection);
                command.Parameters.Add(new SqlParameter("@userId", userId));
                command.Parameters.Add(new SqlParameter("@noteId", noteId));
                connection.Open();
                command.ExecuteNonQuery();
            }                        
        }

        public IEnumerable<int> GetLikes(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT [User] FROM [Likes] WHERE [Note] = @Id", connection);
                command.Parameters.Add(new SqlParameter("@Id", id));
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return (int)reader["User"];
                }
            }   
        }
    }
}
