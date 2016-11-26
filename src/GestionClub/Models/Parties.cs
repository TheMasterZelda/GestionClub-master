using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClub.Models
{
    public class Partie
    {
        public int ID { get; set; }
        public string Numero { get; set; }
        public DateTime DateJouer { get; set; }
        public bool Etat { get; set; }
        public bool Gagnant { get; set; }

        // Foreign Key
        public string UserId1 { get; set; }
        public Participant User1 { get; set; }
        public string UserId2 { get; set; }
        public Participant User2 { get; set; }
        public int TournoiId { get; set; }
        public Tournoi Tournoi { get; set; }
    }

    static public class Parties
    {
        static public List<Partie> ListePartie { get; set; } = new List<Partie>();
    }
}
