using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClub.Models.ForumViewModels
{
    public class ForumViewModel
    {
        public int ID { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "La limite de caractère est de 30.")]
        [Display(Name = "Title")]
        public string Titre { get; set; }
        [StringLength(150, ErrorMessage = "La limite de caractère est de 150.")]
        public string Description { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:d MMMM yyyy}")]
        public DateTime DateCreation { get; set; }
        [Required]
        public int NombreMessage { get; set; }
        // Foreign Variable
        public List<string> MessageAuteur { get; set; }
        [Display(Name = "Message")]
        public List<string> MessageTexte { get; set; }
        [DisplayFormat(DataFormatString = "{0:d MMMM yyyy }")]
        public List<DateTime> MessageDateMessage { get; set; }
        public List<Message> Messages { get; set; }
        [Required]
        public string Auteur { get; set; }
        [Required]
        public string Dernier { get; set; }
        [DisplayFormat(DataFormatString = "{0:d MMMM yyyy}")]
        public DateTime DateModification { get; set; }


        public ForumViewModel()
        {
        }

        public ForumViewModel(Forum forum)
        {
            ID = forum.ID;
            Titre = forum.Titre;
            Description = forum.Description;
           // Auteur = forum.Auteur;
            DateCreation = forum.DateCreation;
            if (forum.User != null)
            {
                Auteur = forum.User.UserName;
            }
            if (forum.Messages != null && forum.Messages.Count != 0)
            {
                MessageAuteur = new List<string>();
                MessageTexte = new List<string>();
                MessageDateMessage = new List<DateTime>();
                Messages = new List<Message>();
                DateModification = forum.Messages.OrderByDescending(m => m.DateMessage).FirstOrDefault().DateMessage;
                Dernier = forum.Dernier;

                foreach (Message m in forum.Messages)
                {
                    MessageAuteur.Add(m.Auteur);
                    MessageTexte.Add(m.Texte);
                    MessageDateMessage.Add(m.DateMessage);
                    Messages.Add(m);
                }
                NombreMessage = forum.Messages.Count();
            }

        }

    }




}
