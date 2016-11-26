using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClub.Models.TournoiViewModels
{
    public class TournoiViewModel
    {
        public int ID { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public string Createur { get; set; }
        public DateTime DateCreation { get; set; }
        public List<string> Prix { get; set; }
        public string Localisation { get; set; }
        public bool Commencer { get; set; }
        public bool Terminer { get; set; }


        public List<Participant> Participants { get; set; }
        public List<Partie> Parties { get; set; }

        // Foreign Variables
        public List<string> NomUtilisateur { get; set; }
        public List<DateTime> DateInscription { get; set; }
        public bool Etat { get; set; }

        public List<string> NumeroPartie { get; set; }
        public List<DateTime> DateJouerPartie { get; set; }
        public List<bool> EtatPartie { get; set; }
        public List<bool> GagnantPartie { get; set; }

        public TournoiViewModel()
        {

        }

        public TournoiViewModel(Tournoi tournoi)
        {

        }
    }
}
