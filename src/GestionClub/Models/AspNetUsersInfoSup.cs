using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClub.Models
{
    public class AspNetUsersInfoSup
    {
        public int ID { get; set; }
        public string Rang { get; set; }
        public Image Image { get; set; }
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
