using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GestionClub.Models;

namespace GestionClub.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Forum> Forums { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ApplicationUser> Membres { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Tournoi> Tournois { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Partie> Parties { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
