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


        //without refferance
        public LabelEntity CreateLabel(LabelModel model, long userId)
        {
            try
            {
                LabelEntity label = new LabelEntity();
                var result = fundoContext.LabelTable.Where(e => e.UserID == userId);
                if (result != null)
                {
                    label.UserID = userId;
                    label.LabelName = model.LabelName;

                    fundoContext.LabelTable.Add(label);
                    fundoContext.SaveChanges();
                    return label;
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

        //public LabelEntity AddLabel(long userID, long notesID, string labelname)
        //{
        //    try
        //    {
        //        var noteResult = fundoContext.LabelTable.Where(x => x.NotesID == notesID).FirstOrDefault();
        //        if (noteResult != null)
        //        {
        //            LabelEntity labelEntity = new LabelEntity();
        //            labelEntity.NotesID = noteResult.NotesID;
        //            labelEntity.UserID = noteResult.UserID;
        //            labelEntity.LabelName = labelname;
        //            fundoContext.Add(labelEntity);
        //            fundoContext.SaveChanges();
        //            return labelEntity;
        //        }
               
        //        else
        //        {
        //            return null; 
        //        }
        //    }



        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public LabelEntity AddLabel(long userID, long notesID, string labelname)
        {
            try
            {
                var labelData = fundoContext.LabelTable.Where(x => x.LabelName == labelname).FirstOrDefault();
                if (labelData != null)
                {
                    //LabelEntity labelEntity = new LabelEntity();
                    //labelEntity.NotesID = labelData.NotesID;
                    //labelEntity.UserID = labelData.UserID;
                    //labelEntity.LabelName = labelname;
                    labelData.NotesID = notesID;
                    //var entity = labelData;
                    fundoContext.Entry(labelData).CurrentValues.SetValues(labelData);
                    //fundoContext.
                    //fundoContext.Update(labelEntity);
                    fundoContext.SaveChanges();
                    return labelData;
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

        public IEnumerable<LabelEntity> GetLabels(long userId)
        {
            try
            {
                var result = fundoContext.LabelTable.ToList().Where(x => x.UserID == userId);
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



        public LabelEntity UpdateLabel(LabelModel labelModel ,long labelID)
        {
            try
            {
                var data = fundoContext.LabelTable.SingleOrDefault(x => x.LabelID == labelID);
                if (data != null)
                { 
                    data.LabelName = labelModel.LabelName;
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

            var result = fundoContext.LabelTable.FirstOrDefault(e => e.LabelID == LabelID);
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
