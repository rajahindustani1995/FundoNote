using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class NotesModel
    {
        public string Title { get; set; }
        public string Discription { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }

        public bool Archive { get; set; }
        public bool Pin { get; set; }
        public bool Trash { get; set; }

        public DateTime Reminder { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedTime { get; set; }


    }
}
