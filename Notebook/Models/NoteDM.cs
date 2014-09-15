using AutoMapper;
using Notebook.Common;
using Notebook.Models.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notebook.Models
{
    public class NoteDM
    {
        public int Id { get; set; }
        public int Author { get; set; }
        public DateTime CreationDate { get; set; }
        public string Content { get; set; }

        public NoteDM() { }

        public static List<NoteDM> GetAll()
        {
            return Mapper.Map<List<NoteDM>>(Global.business.GetAllNotes());
        }

        public static List<NoteDM> GetAll(NoteFilter filter)
        {
            List<NoteDM> notes = GetAll();
            if (filter.MinDate != default(DateTime))
            {
                notes = notes.Where(n => n.CreationDate >= filter.MinDate).ToList();
            }
            if (filter.MaxDate != default(DateTime))
            {
                notes = notes.Where(n => n.CreationDate <= filter.MaxDate).ToList();
            }
            if (!string.IsNullOrWhiteSpace(filter.Authors))
            {
                string[] authors = filter.Authors.Split(';');
                notes = notes.Where(n => authors.Any(a => a == AccountDM.GetUserName(n.Author))).ToList();
            }
            if (!string.IsNullOrWhiteSpace(filter.LikedBy))
            {
                string[] likedBy = filter.LikedBy.Split(';');
                notes = notes.Where(n => likedBy.Any(a => NoteDM.IsLiked(AccountDM.GetUserId(a), n.Id))).ToList();
            }
            return notes;
        }

        public static List<DisplayNoteVM> DisplayAll()
        {
            return Mapper.Map<List<DisplayNoteVM>>(GetAll());
        }

        public static List<DisplayNoteVM> DisplayAll(NoteFilter filter)
        {
            return Mapper.Map<List<DisplayNoteVM>>(GetAll(filter));
        }

        public static void Add(NoteDM note)
        {
            Global.business.AddNote(Mapper.Map<Notebook.Common.Note>(note));
        }

        public static void Delete(int id)
        {
            Global.business.DeleteNote(id);
        }

        public static bool IsLiked(int userId, int noteId)
        {
            return Global.business.GetNoteLikes(noteId).Any(n => n == userId);
        }

        public static void ToggleLike(int userId, int noteId)
        {
            if (IsLiked(userId, noteId))
            {
                Global.business.Dislike(userId, noteId);
            }
            else
            {
                Global.business.Like(userId, noteId);
            }
        }

        public static NoteDM Get(int id)
        {
            return GetAll().First(n => n.Id == id);
        }

        public static void Update(int id, string content)
        {
            Global.business.UpdateNote(id, content);
        }

        public static int LikesCount(int id)
        {
            return Global.business.NoteLikesCount(id);
        }
    }
}