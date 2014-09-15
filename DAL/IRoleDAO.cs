using Notebook.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.DAL
{
    public interface IRoleDAO
    {
        void Dispose();
        void Add(string role);
        IEnumerable<Role> GetAll();
        bool RoleExists(string role);
        IEnumerable<Role> GetRolesForUser(int userId);
        int GetId(string role);
        bool IsUserInRole(int userId, string role);
        void AddUserToRole(int userId, string role);
    }
}
