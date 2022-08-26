using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {
        public NotesEntity Create(NotesModel model, long userId);
        public IEnumerable<NotesEntity> Retrieve(long NotesID);
        public NotesEntity UpdateNote(NotesModel model, long NotesID);
        public string Delete(long NotesID);
    }
}
