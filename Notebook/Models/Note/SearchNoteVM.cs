using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Notebook.Models.Note
{
    public class SearchNoteVM
    {
        [Display(Name="lblMinDate", ResourceType=typeof(Resources))]
        public DateTime MinDate { get; set; }
        [Display(Name = "lblMaxDate", ResourceType = typeof(Resources))]
        public DateTime MaxDate { get; set; }
        [Display(Name = "lblAuthoredBy", ResourceType = typeof(Resources))]
        public string Authors { get; set; }
        [Display(Name = "lblLikedBy", ResourceType = typeof(Resources))]
        public string LikedBy { get; set; }
    }
}