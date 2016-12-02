using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClub.Models
{
    public class Tournoi
    {
        public int ID { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public string Createur { get; set; }
        public DateTime DateCreation { get; set; }
        public string Prix { get; set; }
        public  string Localisation { get; set; }
        public bool Start { get; set; }
        public bool State { get; set; }
        public int NombrePartie { get; set; }

        // Foreign Keys
        public List<Participant> Participants { get; set; }
        public List<Partie> Parties { get; set; }
    }

    static public class Tournois
    {
        static public List<Tournoi> ListeTournoi { get; set; } = new List<Tournoi>();
    }
}
