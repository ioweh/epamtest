using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Notebook.Models.Note
{
    public class DisplayNoteVM
    {
        public int Id { get; set; }
        public int Author { get; set; }
        public DateTime CreationDate { get; set; }
        public string Content { get; set; }
        public bool Collapsed { get; set; }
    }
}