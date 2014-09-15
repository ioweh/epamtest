using AutoMapper;
using Notebook.Common;
using Notebook.Models;
using Notebook.Models.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notebook
{
    public class AutoMapperConfig
    {
        public static void RegisterMaps()
        {
            Mapper.CreateMap<User, AccountDM>();
            Mapper.CreateMap<AccountDM, User>();

            Mapper.CreateMap<Note, NoteDM>();
            Mapper.CreateMap<NoteDM, Note>();

            Mapper.CreateMap<DisplayNoteVM, NoteDM>();
            Mapper.CreateMap<NoteDM, DisplayNoteVM>().ForMember(dest => dest.Collapsed, opt => opt.Ignore());

            Mapper.CreateMap<CreateNoteVM, NoteDM>().
                ForMember(dest => dest.Author, opt => opt.Ignore()).
                ForMember(dest => dest.CreationDate, opt => opt.Ignore()).
                ForMember(dest => dest.Id, opt => opt.Ignore());

            Mapper.CreateMap<NoteFilter, SearchNoteVM>();
            Mapper.CreateMap<SearchNoteVM, NoteFilter>();

            Mapper.AssertConfigurationIsValid();
        }
    }
}