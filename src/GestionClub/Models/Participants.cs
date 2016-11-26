using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClub.Models
{
    public class Participant
    {
        public int ID { get; set; }
        public string NomUtilisateur { get; set; }
        public string Prénom { get; set; }
        public string Nom { get; set; }
        public DateTime DateInscription { get; set; }
        public bool Etat { get; set; }

        // Foreign Key
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }

    static public class Participants
    {
        static public List<Participant> ListeParticipant { get; set; } = new List<Participant>();
    }
}
