
using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }
        public LabelEntity AddLabel(long userID, long notesID, string labelname)
        {
            try
            {
                return labelRL.AddLabel(userID, notesID, labelname);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public LabelEntity CreateLabel(LabelModel model, long userId)
        {
            try
            {
                return labelRL.CreateLabel(model, userId);
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
        //        return labelRL.AddLabel(userID, notesID, labelname);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public LabelEntity CreateLabel(LabelModel model, long userId)
        //{
        //    try
        //    {
        //        return labelRL.CreateLabel(model, userId);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        public IEnumerable<LabelEntity> GetLabels(long userId)
        {
            try
            {
                return labelRL.GetLabels(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public LabelEntity UpdateLabel(LabelModel labelModel, long labelID)
        {
            try
            {
                return labelRL.UpdateLabel(labelModel,labelID);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public string Delete(long LabelID)
        {
            try
            {
                return labelRL.Delete(LabelID);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
