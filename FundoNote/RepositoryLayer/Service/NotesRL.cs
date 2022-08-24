using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NotesRL:INotesRL
    {
        private readonly FundoContext fundoContext;
        public NotesRL(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }
    }
}
