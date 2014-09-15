using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.Common
{
    public class Note
    {
        public int Id { get; set; }
        public int Author { get; set; }
        public DateTime CreationDate { get; set; }
        public string Content { get; set; }

        public Note() { }
    }
}
