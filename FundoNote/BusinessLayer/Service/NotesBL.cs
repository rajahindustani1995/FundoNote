﻿using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NotesBL : INotesBL
    {
        private readonly INotesRL notesRL;
        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }
        public NotesEntity Create(NotesModel model, long userId)
        {
            try
            {
                return notesRL.Create(model, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<NotesEntity> Retrieve(long NotesID)
        {
            try
            {
                return notesRL.Retrieve(NotesID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public NotesEntity UpdateNote(NotesModel model, long NotesID)
        {
            try
            {
                return notesRL.UpdateNote(model, NotesID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string Delete(long NotesID)
        {
            try
            {
                return notesRL.Delete(NotesID);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}