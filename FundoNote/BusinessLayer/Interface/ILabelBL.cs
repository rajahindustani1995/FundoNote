using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public LabelEntity Create(LabelModel labelModel, long userID, long notesID);
        //public string Delete(long LabelID);
    }
}
