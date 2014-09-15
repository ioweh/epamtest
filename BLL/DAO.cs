using Notebook.DAL;
using Notebook.DAL.MSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.BLL
{
    public class DAO : IDisposable
    {
        public INoteDAO NoteAccess { get; private set; }
        public IUserDAO UserAccess { get; private set; }
        public IRoleDAO RoleAccess { get; private set; }

        public DAO(string connectionString)
        {
            NoteAccess = new MSQLNoteDAO(connectionString);
            UserAccess = new MSQLUserDAO(connectionString);
            RoleAccess = new MSQLRoleDAO(connectionString);
        }

        public void Dispose()
        {
            NoteAccess.Dispose();
            UserAccess.Dispose();
            RoleAccess.Dispose();
        }
    }
}
