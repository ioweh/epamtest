using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.Common
{
    public class NoteFilter
    {
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
        public string Authors { get; set; }
        public string LikedBy { get; set; }

        public NoteFilter() {
            Authors = "";
        }
    }
}
