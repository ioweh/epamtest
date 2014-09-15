using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Notebook.Models.Note
{
    public class CreateNoteVM
    {
        [Required(ErrorMessageResourceType=typeof(ErrorMsg), ErrorMessageResourceName="ContentMinLength")]
        [MaxLength(1000, ErrorMessageResourceType=typeof(ErrorMsg), ErrorMessageResourceName="ContentMaxLength")]
        [Display(Name = "createPrompt", ResourceType=typeof(Resources))]
        public string Content { get; set; }
    }
}