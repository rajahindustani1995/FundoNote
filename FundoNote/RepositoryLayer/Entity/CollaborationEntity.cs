using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entity
{
    public class CollaborationEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollaboratorID { get; set; }
        public string CollaboratorEmail { get; set; }

        [ForeignKey("User")]
        public long UserID { get; set; }
        public virtual UserEntity User { get; set; }


        [ForeignKey("Notes")]
        public long NotesID { get; set; }
        public virtual NotesEntity Notes { get; set; }
    }
}
