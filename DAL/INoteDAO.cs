using Notebook.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.DAL
{
    public interface INoteDAO : IDisposable
    {
        IEnumerable<Note> GetAll();
        void Add(Note note);
        void Delete(int id);
        IEnumerable<int> GetLikes(int id);
        void Like(int userId, int noteId);
        void Dislike(int userId, int noteId);
        void Update(int id, string content);
    }
}
