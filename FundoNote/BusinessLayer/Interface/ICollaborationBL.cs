using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollaborationBL
    {
        public CollaborationEntity Create(string Email, long notesID);
        public IEnumerable<CollaborationEntity> Retrieve(long notesID);
        public string Delete(long CollaboratorID);
    }
}
