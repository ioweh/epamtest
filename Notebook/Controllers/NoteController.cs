using AutoMapper;
using Notebook.Common;
using Notebook.Helpers;
using Notebook.Models;
using Notebook.Models.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Notebook.Controllers
{
    public partial class NoteController : Controller
    {
        [Authorize]
        public virtual ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public virtual ActionResult Create(CreateNoteVM note, string hiddenTags)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    NoteDM _note = Mapper.Map<NoteDM>(note);
                    _note.CreationDate = DateTime.Now;
                    _note.Author = AccountDM.GetUserId(User.Identity.Name);
                    NoteDM.Add(_note);
                    return RedirectToAction("Index", "Home");
                }
                return View(note);
            }
            catch (Exception e)
            {
                Logger.Log("Create form post failed", e);
                return View("~/Views/Shared/Error.cshtml");                
            }
        }

        [Authorize]
        public virtual ActionResult Search()
        {
            try
            {
                return View(Mapper.Map<SearchNoteVM>(Global.filter));
            }
            catch (Exception e)
            {
                Logger.Log("Search notes failed", e);
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        [Authorize]
        [HttpPost]
        public virtual ActionResult Search(SearchNoteVM searchFilter)
        {
            try
            {
                Global.filter = Mapper.Map<NoteFilter>(searchFilter);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                Logger.Log("Search form post failed", e);
                return View("~/Views/Shared/Error.cshtml");                
            }
        }

        [Authorize]
        [HttpPost]
        public virtual ActionResult Delete(int id)
        {
            try
            {
                NoteDM.Delete(id);
                if (Request.IsAjaxRequest())
                {
                    if (NoteDM.GetAll(Global.filter).Count == 0)
                    {
                        return Content("<h3>" + Resources.mgsNothingFound + "</h3>");
                    }
                    else
                    {
                        return Content("");
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                Logger.Log("Delete note failed", e);
                return View("~/Views/Shared/Error.cshtml");                
            }
        }

        [Authorize]
        [HttpPost]
        public virtual ActionResult ToggleLike(int userId, int noteId)
        {
            try
            {
                NoteDM.ToggleLike(userId, noteId);
                return PartialView("~/Views/Note/Single.cshtml", Mapper.Map<DisplayNoteVM>(NoteDM.Get(noteId)));
            }
            catch (Exception e)
            {
                Logger.Log("Like note failed", e);
                return View("~/Views/Shared/Error.cshtml");
            }            
        }

        [Authorize]
        [HttpPost]
        public virtual ActionResult Edit(int id)
        {
            try
            {
                return PartialView("~/Views/Note/EditNote.cshtml", NoteDM.Get(id).Content);
            }
            catch (Exception e)
            {
                Logger.Log("Edit note failed", e);
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        [Authorize]
        [HttpPost]
        public virtual ActionResult Save(int id, string content)
        {
            try
            {
                NoteDM.Update(id, content);
                return PartialView("~/Views/Note/ShowNote.cshtml", content);
            }
            catch (Exception e)
            {
                Logger.Log("Save note failed", e);
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}