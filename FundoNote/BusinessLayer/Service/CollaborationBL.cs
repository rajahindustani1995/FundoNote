﻿using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CollaborationBL : ICollaborationBL
    {
        private readonly ICollaborationRL collabRL;
        public CollaborationBL(ICollaborationRL collabRL)
        {
            this.collabRL = collabRL;
        }
        public CollaborationEntity Create(string Email, long notesID)
        {
            try
            {
                return collabRL.Create(Email, notesID);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
