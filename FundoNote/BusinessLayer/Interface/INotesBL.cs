using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INotesBL
    {
        public NotesEntity Create(NotesModel model, long userId);
        public IEnumerable<NotesEntity> Retrieve(long userId);
        public NotesEntity UpdateNote(NotesModel model, long NotesID);
        public string Delete(long NotesID);
        public NotesEntity Archive(long NotesID);
        public NotesEntity Pin(long NotesID);
        public NotesEntity Trash(long NotesID);
        public NotesEntity ChoiceColor(long NotesID, string Color);
        public string Image(IFormFile image, long noteID, long userID);
    }
}
