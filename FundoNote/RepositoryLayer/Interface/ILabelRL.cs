using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public LabelEntity Create(LabelModel labelModel, long userID, long notesID);
        public string Delete(long LabelID);
        public IEnumerable<LabelEntity> Retrieve(long NotesID);
    }
}
