using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollaborationRL
    {
        public CollaborationEntity Create(string Email, long notesID);
        public IEnumerable<CollaborationEntity> Retrieve(long notesID);
        public IEnumerable<CollaborationEntity> GetAllCollab(long userID);
        public string Delete(long CollaboratorID);
    }
}
