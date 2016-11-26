using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClub.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Type { get; set; }
        public int Taille { get; set; }
        public byte[] Data { get; set; }
    }
}

