using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClub.Models.MessageViewModels
{
    public class MessageViewModel
    {
        public int ID { get; set; }
        [Required]
        [StringLength(1000, ErrorMessage = "La limite de caractère est de 150.")]
        public string Texte { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:d MMMM yyyy }")]
        public DateTime DateMessage { get; set; }

        // Foreign Variable
        [Required]
        public int ForumID { get; set; }
        public Forum Frm { get; set; }
        [Required]
        public string Auteur { get; set; }
        public string ImageType { get; set; }
        public string ImageNom { get; set; }
        [Display(Name = "Image")]
        public Byte[] ImageData { get; set; }

        public MessageViewModel()
        {
        }

        public MessageViewModel(Message message)
        {
            ID = message.ID;
            Texte = message.Texte;
            DateMessage = message.DateMessage;
            ForumID = message.ForumID;
            Frm = message.Frm;
            if (message.User != null)
            {
                Auteur = message.User.UserName;
                if (message.User.InfoSup.Image != null)
                {
                    ImageType = message.User.InfoSup.Image.Type;
                    ImageNom = message.User.InfoSup.Image.Nom;
                    ImageData = message.User.InfoSup.Image.Data;
                }
            }
        }
    }
}
