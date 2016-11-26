using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GestionClub.Data;

namespace GestionClub.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161126204929_lethality")]
    partial class lethality
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GestionClub.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("GestionClub.Models.AspNetUsersInfoSup", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ImageId");

                    b.Property<string>("Rang");

                    b.Property<string>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("ImageId");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("AspNetUsersInfoSup");
                });

            modelBuilder.Entity("GestionClub.Models.Forum", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Auteur");

                    b.Property<DateTime>("DateCreation");

                    b.Property<DateTime>("DateModification");

                    b.Property<string>("Dernier");

                    b.Property<string>("Description");

                    b.Property<int>("NombreMessage");

                    b.Property<string>("Titre");

                    b.Property<string>("UserId");

                    b.HasKey("ID");

                    b.HasIndex("UserId");

                    b.ToTable("Forums");
                });

            modelBuilder.Entity("GestionClub.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Data");

                    b.Property<string>("Nom");

                    b.Property<int>("Taille");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("GestionClub.Models.Message", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Auteur");

                    b.Property<DateTime>("DateMessage");

                    b.Property<int>("ForumID");

                    b.Property<string>("Texte");

                    b.Property<string>("UserId");

                    b.HasKey("ID");

                    b.HasIndex("ForumID");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("GestionClub.Models.Participant", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateInscription");

                    b.Property<bool>("Etat");

                    b.Property<string>("Nom");

                    b.Property<string>("NomUtilisateur");

                    b.Property<string>("Prénom");

                    b.Property<int?>("TournoiID");

                    b.Property<string>("UserId");

                    b.HasKey("ID");

                    b.HasIndex("TournoiID");

                    b.HasIndex("UserId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("GestionClub.Models.Partie", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateJouer");

                    b.Property<bool>("Etat");

                    b.Property<bool>("Gagnant");

                    b.Property<string>("Numero");

                    b.Property<int>("TournoiId");

                    b.Property<int?>("User1ID");

                    b.Property<int?>("User2ID");

                    b.Property<string>("UserId1");

                    b.Property<string>("UserId2");

                    b.HasKey("ID");

                    b.HasIndex("TournoiId");

                    b.HasIndex("User1ID");

                    b.HasIndex("User2ID");

                    b.ToTable("Parties");
                });

            modelBuilder.Entity("GestionClub.Models.Tournoi", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Createur");

                    b.Property<DateTime>("DateCreation");

                    b.Property<string>("Description");

                    b.Property<string>("Localisation");

                    b.Property<string>("Prix");

                    b.Property<bool>("Start");

                    b.Property<bool>("State");

                    b.Property<string>("Titre");

                    b.HasKey("ID");

                    b.ToTable("Tournois");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("GestionClub.Models.AspNetUsersInfoSup", b =>
                {
                    b.HasOne("GestionClub.Models.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.HasOne("GestionClub.Models.ApplicationUser", "User")
                        .WithOne("InfoSup")
                        .HasForeignKey("GestionClub.Models.AspNetUsersInfoSup", "UserID");
                });

            modelBuilder.Entity("GestionClub.Models.Forum", b =>
                {
                    b.HasOne("GestionClub.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("GestionClub.Models.Message", b =>
                {
                    b.HasOne("GestionClub.Models.Forum", "Frm")
                        .WithMany("Messages")
                        .HasForeignKey("ForumID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestionClub.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("GestionClub.Models.Participant", b =>
                {
                    b.HasOne("GestionClub.Models.Tournoi")
                        .WithMany("Participants")
                        .HasForeignKey("TournoiID");

                    b.HasOne("GestionClub.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("GestionClub.Models.Partie", b =>
                {
                    b.HasOne("GestionClub.Models.Tournoi", "Tournoi")
                        .WithMany("Parties")
                        .HasForeignKey("TournoiId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestionClub.Models.Participant", "User1")
                        .WithMany()
                        .HasForeignKey("User1ID");

                    b.HasOne("GestionClub.Models.Participant", "User2")
                        .WithMany()
                        .HasForeignKey("User2ID");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("GestionClub.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("GestionClub.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestionClub.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
