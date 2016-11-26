using GestionClub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClub.Data.Migrations
{
    public class SeedData
    {
        #region Champs
        static PasswordHasher<ApplicationUser> _pass = new PasswordHasher<ApplicationUser>();
        #endregion
        #region Propriétés
        static public ApplicationDbContext Context { get; set; }
        #endregion

        #region Méthodes
        static public void AjouterMembre()
        {
            ApplicationUser[] users =
            {
                new ApplicationUser()
                {
                    Email = "admin@rnd.ca",
                    PasswordHash = "adminA123!"
                },
                new ApplicationUser()
                {
                    Email = "user@rnd.ca",
                    PasswordHash = "userA123!"
                },
                new ApplicationUser()
                {
                    Email = "mod@rnd.ca",
                    PasswordHash = "modA123!"
                },
                new ApplicationUser()
                {
                    Email = "ban@rnd.ca",
                    PasswordHash = "banA123!"
                },
                new ApplicationUser()
                {
                    Email = "gm@rnd.ca",
                    PasswordHash = "gmA123!"
                },
                new ApplicationUser()
                {
                    Email = "gm2@rnd.ca",
                    PasswordHash = "gmA123!"
                }
            };

            foreach (ApplicationUser u in users)
            {
                u.ConcurrencyStamp = Guid.NewGuid().ToString();
                u.LockoutEnabled = true;
                u.NormalizedEmail = u.Email.ToUpper();
                u.NormalizedUserName = u.Email.ToUpper();
                u.SecurityStamp = Guid.NewGuid().ToString();
                u.UserName = u.Email;
                u.PasswordHash = _pass.HashPassword(u, u.PasswordHash);
                u.InfoSup = new AspNetUsersInfoSup()
                {
                    UserID = u.Id
                };
            }

            foreach (ApplicationUser user in users)
            {
                if (!Context.Users.Any(u => u.Email == user.Email))
                    Context.Users.Add(user);
            }
            Context.SaveChanges();

        }

        public static void AjouterRoles()
        {
            IdentityRole[] roles =
            {
                new IdentityRole()
                {
                    Name = "Administrateur"
                },
                new IdentityRole()
                {
                    Name = "Utilisateur"
                },
                new IdentityRole()
                {
                    Name = "Modérateur"
                },
                new IdentityRole()
                {
                    Name = "ExclustionForum"
                },
                new IdentityRole()
                {
                    Name = "GrandMaster"
                }

            };

            foreach (IdentityRole role in roles)
            {
                role.NormalizedName = role.Name.ToUpper();
                role.ConcurrencyStamp = Guid.NewGuid().ToString();
            }

            foreach (IdentityRole role in roles)
            {
                if (!Context.Roles.Any(r => r.NormalizedName == role.NormalizedName))
                    Context.Roles.Add(role);
            }
            Context.SaveChanges();
            #endregion
        }

        public static void AssocierRolesUsagers()
        {
            IdentityUserRole<string>[] users_role =
            {
                new IdentityUserRole<string>()
                {
                    UserId = Context.Users.Single(u => u.Email == "admin@rnd.ca").Id,
                    RoleId = Context.Roles.Single(r => r.Name == "Administrateur").Id
                },
                 new IdentityUserRole<string>()
                {
                    UserId = Context.Users.Single(u => u.Email == "user@rnd.ca").Id,
                    RoleId = Context.Roles.Single(r => r.Name == "Utilisateur").Id
                },
                 new IdentityUserRole<string>()
                {
                    UserId = Context.Users.Single(u => u.Email == "mod@rnd.ca").Id,
                    RoleId = Context.Roles.Single(r => r.Name == "Modérateur").Id
                },
                 new IdentityUserRole<string>()
                {
                    UserId = Context.Users.Single(u => u.Email == "ban@rnd.ca").Id,
                    RoleId = Context.Roles.Single(r => r.Name == "ExclustionForum").Id
                },
                 new IdentityUserRole<string>()
                {
                    UserId = Context.Users.Single(u => u.Email == "gm@rnd.ca").Id,
                    RoleId = Context.Roles.Single(r => r.Name == "GrandMaster").Id
                }

            };

            if (!Context.UserRoles.Any())
            {
                Context.UserRoles.AddRange(users_role);
                Context.SaveChanges();
            }
        }
    }
}
