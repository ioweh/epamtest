using Notebook.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.DAL
{
    public interface IUserDAO : IDisposable
    {
        IEnumerable<User> GetAll();
        void Add(User user);
        bool UserExists(string name);
    }
}
