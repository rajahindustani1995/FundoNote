using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public LabelEntity AddLabel(long userID, long notesID, string labelname);
        public LabelEntity CreateLabel(LabelModel model, long userId);
        //public string AddLabel(LabelEntity label, long NotesID);
        //public string CreateLabel(LabelEntity label);
        public string Delete(long LabelID);
        public IEnumerable<LabelEntity> GetLabels(long notesId);
        public LabelEntity UpdateLabel(LabelModel labelModel, long labelID);
    }
}
