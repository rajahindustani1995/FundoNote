using BusinessLayer.Interface;
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
        public IEnumerable<CollaborationEntity> Retrieve(long notesID)
        {
            try
            {
                return collabRL.Retrieve(notesID);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<CollaborationEntity> GetAllCollab(long userID)
        {
            try
            {
                return collabRL.GetAllCollab(userID);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Delete(long CollaboratorID)
        {
            try
            {
                return collabRL.Delete(CollaboratorID);

            }
            catch (Exception)
            { 

                throw;
            }
        }
    }
}
