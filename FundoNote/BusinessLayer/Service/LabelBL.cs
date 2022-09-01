
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
        public LabelEntity Create(LabelModel labelModel, long userID, long notesID)
        {
            try
            {
                return labelRL.Create(labelModel, userID, notesID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public string Delete(long LabelID)
        //{
        //    try
        //    {
        //        return labelRL.Delete(LabelID);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
