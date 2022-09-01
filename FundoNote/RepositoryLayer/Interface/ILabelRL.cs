﻿using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public LabelEntity Create(long userID, long notesID, string labelname);
        public string Delete(long LabelID);
        public IEnumerable<LabelEntity> Retrieve(long LabelID);
        public LabelEntity UpdateLabel(long labelID, string labelname);
    }
}
