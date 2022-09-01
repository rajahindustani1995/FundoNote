using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabelRL
    {
        private readonly FundoContext fundoContext;

        public LabelRL(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }
        public LabelEntity Create(LabelModel labelModel, long userID, long notesID)
        {
            try
            {
                LabelEntity labelEntity = new LabelEntity();

                labelEntity.LabelName = labelModel.LabelName;
                labelEntity.NotesID = notesID;
                labelEntity.UserID = userID;

                fundoContext.LabelTable.Add(labelEntity);
                int result = fundoContext.SaveChanges();
                if (result > 0)
                {
                    return labelEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        //public string Delete(long LabelID)
        //{

        //    var result = fundoContext.LabelTable.FirstOrDefault(e => e.NotesID == LabelID);
        //    if (result != null)
        //    {
        //        fundoContext.LabelTable.Remove(result);
        //        fundoContext.SaveChanges();
        //        return "Label Delete Successfull";
        //    }
        //    else
        //    {
        //        return "Label Does not Delete";
        //    }
        //}
    }
}
