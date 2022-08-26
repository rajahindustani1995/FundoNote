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
    public class NotesRL : INotesRL
    {
        private readonly FundoContext fundoContext;
        public NotesRL(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }

        public NotesEntity Create(NotesModel model, long userId)
        {
            try
            {
                NotesEntity notes = new NotesEntity();
                var result = fundoContext.NotesTable.Where(e => e.UserID == userId);
                if (result != null)
                {
                    notes.UserID = userId;
                    notes.Title = model.Title;
                    notes.Discription = model.Discription;
                    notes.Color = model.Color;
                    notes.Reminder = model.Reminder;
                    notes.Image = model.Image;
                    notes.Archive = model.Archive;
                    notes.Pin = model.Pin;
                    notes.Trash = model.Trash;
                    notes.CreatedTime = model.CreatedTime;
                    notes.ModifiedTime = model.ModifiedTime;
                    fundoContext.NotesTable.Add(notes);
                    fundoContext.SaveChanges();
                    return notes;
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
        public IEnumerable<NotesEntity> Retrieve(long NotesID)
        {
            try
            {
                var result = fundoContext.NotesTable.SingleOrDefault(e => e.NotesID == NotesID);
                List<NotesEntity> list = fundoContext.NotesTable.ToList();
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

        public NotesEntity UpdateNote(NotesModel model, long NotesID)
        {
            try
            {
                var data = fundoContext.NotesTable.SingleOrDefault(x => x.NotesID == NotesID);
                if (data != null)
                {
                    data.Title = model.Title;
                    data.Discription = model.Discription;
                    data.Color = model.Color;
                    data.Reminder = model.Reminder;
                    data.Image = model.Image;
                    data.Archive = model.Archive;
                    data.Pin = model.Pin;
                    data.Trash = model.Trash;
                    data.CreatedTime = model.CreatedTime;
                    data.ModifiedTime = model.ModifiedTime;
                    fundoContext.NotesTable.Update(data);
                    fundoContext.SaveChanges();
                    return data;
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
        public string Delete(long NotesID)
        {
            try
            {
                var result = fundoContext.NotesTable.FirstOrDefault(e => e.NotesID == NotesID);
                if (result != null)
                {
                    fundoContext.NotesTable.Remove(result);
                    fundoContext.SaveChanges();
                    return "Notes Deleted Successfull";
                }
                else
                {
                    return "Notes Unable to Delete";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NotesEntity Pin(long NotesID)
        {
            NotesEntity data = fundoContext.NotesTable.FirstOrDefault(x => x.NotesID == NotesID);
            if (data.Pin == true)
            {
                data.Pin = false;

                fundoContext.SaveChanges();
                return data;
            }
            else
            {
                return null;
            }
        }

        public NotesEntity Trash(long NotesID)
        {
            NotesEntity data = fundoContext.NotesTable.FirstOrDefault(x => x.NotesID == NotesID);
            if (data.Trash == true)
            {
                data.Trash = false;
                fundoContext.SaveChanges();
                return data;
            }
            else
            {
                return null;
            }
        }
    }

}
