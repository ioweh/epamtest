using Notebook.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.BLL
{
    public class Business
    {
        public DAO Dao { get; private set; }

        public Business(string connectionString)
        {
            Dao = new DAO(connectionString);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return Dao.UserAccess.GetAll();
        }

        public void CreateUser(User user)
        {
            Dao.UserAccess.Add(user);
        }

        public bool UserExists(string name)
        {
            return Dao.UserAccess.UserExists(name);
        }

        public IEnumerable<Note> GetAllNotes()
        {
            return Dao.NoteAccess.GetAll();
        }

        public void AddNote(Note note)
        {
            Dao.NoteAccess.Add(note);
        }

        public void DeleteNote(int id)
        {
            Dao.NoteAccess.Delete(id);
        }

        public IEnumerable<int> GetNoteLikes(int noteId)
        {
            return Dao.NoteAccess.GetLikes(noteId);
        }

        public void Like(int userId, int noteId)
        {
            Dao.NoteAccess.Like(userId, noteId);
        }

        public int NoteLikesCount(int noteId)
        {
            return GetNoteLikes(noteId).Count();
        }

        public void Dislike(int userId, int noteId)
        {
            Dao.NoteAccess.Dislike(userId, noteId);
        }

        public void UpdateNote(int id, string content)
        {
            Dao.NoteAccess.Update(id, content);
        }

        public void CreateRole(string role)
        {
            Dao.RoleAccess.Add(role);
        }

        public IEnumerable<Role> GetRoles()
        {
            return Dao.RoleAccess.GetAll();
        }

        public bool RoleExists(string role)
        {
            return Dao.RoleAccess.RoleExists(role);
        }

        public IEnumerable<Role> GetRolesForUser(int userId)
        {
            return Dao.RoleAccess.GetRolesForUser(userId);
        }

        public int GetRoleId(string role)
        {
            return Dao.RoleAccess.GetId(role);
        }

        public bool IsUserInRole(int userId, string role)
        {
            return Dao.RoleAccess.IsUserInRole(userId, role);
        }

        public void AddUserToRole(int userId, string role)
        {
            Dao.RoleAccess.AddUserToRole(userId, role);
        }
    }
}
