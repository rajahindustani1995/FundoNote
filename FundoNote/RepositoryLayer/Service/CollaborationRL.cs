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
    public class CollaborationRL : ICollaborationRL
    {
        private readonly FundoContext fundoContext;

        public CollaborationRL(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }

        public CollaborationEntity Create(string Email, long notesID)
        {
            try
            {
                var notesModel = fundoContext.NotesTable.Where(x => x.NotesID == notesID).FirstOrDefault();
                var userModel = fundoContext.UserTable.Where(x => x.Email == Email).FirstOrDefault();
                if (notesModel != null && userModel != null)
                {
                    CollaborationEntity collabEntity = new CollaborationEntity();
                    collabEntity.UserID = userModel.UserId;
                    collabEntity.NotesID = notesID;
                    collabEntity.CollaboratorEmail = Email;
                    fundoContext.Add(collabEntity);
                    fundoContext.SaveChanges();
                    return collabEntity;
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

        public IEnumerable<CollaborationEntity> Retrieve(long notesID)
        {
            try
            {
                var result = fundoContext.CollaboratorTable.SingleOrDefault(x => x.NotesID == notesID);
                List<CollaborationEntity> list = fundoContext.CollaboratorTable.ToList();
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
        public string Delete(long CollaboratorID)
        {
            try
            {
                var result = fundoContext.CollaboratorTable.FirstOrDefault(e => e.CollaboratorID == CollaboratorID);
                if (result != null)
                {
                    fundoContext.CollaboratorTable.Remove(result);
                    fundoContext.SaveChanges();
                    return "Collaborator Deleted Successfull";
                }
                else
                {
                    return "Collaborator Unable to Delete";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
