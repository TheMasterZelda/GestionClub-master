using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClub.Models.MembreViewModels
{
    public class MembreViewModel
    {
        public string ID { get; set; }
        public string Email { get; set; }
        [Display(Name = "Nom d'utilisateur")]
        public string NomUtilisateur { get; set; }
        public string Rang { get; set; }
        public string Role { get; set; }
        // Champs pour image
        public string ImageType { get; set; }
        public string ImageNom { get; set; }
        [Display(Name = "Image")]
        public Byte[] ImageData { get; set; }
        public IFormFile Fichier { get; set; }


        public MembreViewModel()
        {
        }

        public MembreViewModel(ApplicationUser Membre)
        {
            ID = Membre.Id;
            Email = Membre.Email;
            NomUtilisateur = Membre.UserName;
            if (Membre.InfoSup != null)
            {
                if(Membre.InfoSup.Image != null)
                {
                    ImageType = Membre.InfoSup.Image.Type;
                    ImageNom = Membre.InfoSup.Image.Nom;
                    ImageData = Membre.InfoSup.Image.Data;
                }
                
                Rang = Membre.InfoSup.Rang;
            }
        }

    }
}
