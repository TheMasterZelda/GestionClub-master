using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClub.Models
{
    public class Forum
    {
        public int ID { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public string Auteur { get; set; }
        public DateTime DateCreation { get; set; }
        public string Dernier { get; set; }
        public DateTime DateModification { get; set; }
        public int NombreMessage { get; set; }
        public List<Message> Messages { get; set; }

        // Foreign Key
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }

    static public class Forums
    {
        static public List<Forum> ListeForum { get; set; } = new List<Forum>();
    }
}
