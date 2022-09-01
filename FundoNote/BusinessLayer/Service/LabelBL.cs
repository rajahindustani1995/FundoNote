
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
        public LabelEntity Create(long userID, long notesID, string labelname)
        {
            try
            {
                return labelRL.Create(userID, notesID, labelname);
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
                return labelRL.Retrieve(LabelID);
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
                return labelRL.UpdateLabel(labelID, labelname);
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
