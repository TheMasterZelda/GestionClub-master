using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClub.Models
{
    public class Message
    {
        public int ID { get; set; }
        public string Auteur { get; set; }
        public string Texte { get; set; }
        public DateTime DateMessage { get; set; }
        
        // Foreign key
        public int ForumID { get; set; }
        public Forum Frm { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
    static public class Messages
    {
        static public List<Message> ListeMessage { get; set; } = new List<Message>();
    }
}
