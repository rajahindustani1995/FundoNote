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
        public LabelEntity Create(long userID, long notesID, string labelname)
        {
            try
            {
                var noteResult = fundoContext.NotesTable.Where(x => x.NotesID == notesID).FirstOrDefault();
                if (noteResult != null)
                {
                    LabelEntity labelEntity = new LabelEntity();
                    labelEntity.NotesID = noteResult.NotesID;
                    labelEntity.UserID = noteResult.UserID;
                    labelEntity.LabelName = labelname;
                    fundoContext.Add(labelEntity);
                    fundoContext.SaveChanges();
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

        public IEnumerable<LabelEntity> Retrieve(long LabelID)
        {
            try
            {
                var result = fundoContext.LabelTable.SingleOrDefault(e => e.LabelID == LabelID);
                List<LabelEntity> list = fundoContext.LabelTable.ToList();
                if (result != null)
                {
                    return list;
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
        public LabelEntity UpdateLabel(long labelID, string labelname)
        {
            try
            {
                var data = fundoContext.LabelTable.SingleOrDefault(x => x.LabelID == labelID);
                if (data != null)
                { 
                    data.LabelName = labelname;
                    fundoContext.LabelTable.Update(data);
                    fundoContext.SaveChanges();
                    return data;
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
        public string Delete(long LabelID)
        {

            var result = fundoContext.LabelTable.FirstOrDefault(e => e.NotesID == LabelID);
            if (result != null)
            {
                fundoContext.LabelTable.Remove(result);
                fundoContext.SaveChanges();
                return "Label Delete Successfull";
            }
            else
            {
                return "Label Does not Delete";
            }
        }
    }
}
