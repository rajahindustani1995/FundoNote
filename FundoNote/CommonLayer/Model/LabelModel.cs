using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Model
{
    public class LabelModel
    {
        [Key]
        public long LabelID { get; set; }
        
        [Required]
        public string LabelName { get; set; }

        [ForeignKey("UserRegistrationModel")]

        public long UserID { get; set; }

        [ForeignKey("NotesModel")]

        public long NotesID { get; set; }
    }
}
