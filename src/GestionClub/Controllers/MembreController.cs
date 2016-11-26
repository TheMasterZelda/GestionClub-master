using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestionClub.Data;
using GestionClub.Models.MembreViewModels;
using Microsoft.EntityFrameworkCore;
using GestionClub.Models;
using Microsoft.AspNetCore.Authorization;

namespace GestionClub.Controllers
{
    public class MembreController : Controller
    {
        private ApplicationDbContext _context = null;
        public MembreController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Membre
        public ActionResult Index()
        {
            List<MembreViewModel> liste_vm = new List<MembreViewModel>();

            var membres = _context.Membres
                          .Include(s => s.InfoSup)
                          .Include(i => i.InfoSup.Image);

            foreach (ApplicationUser m in membres)
            {
                liste_vm.Add(new MembreViewModel(m));
            }
            return View(liste_vm);
        }

        // GET: Membre/Details/5
        public ActionResult Details(string id)
        {
            MembreViewModel userVM = new MembreViewModel(_context.Membres.Include(s => s.InfoSup).FirstOrDefault(u => u.Id == id));
            return View(userVM);
        }

        // GET: Membre/Create
        [Authorize(Roles = "Administrateur")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Membre/Create
        [Authorize(Roles = "Administrateur")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                ApplicationUser user = new ApplicationUser();
                TryUpdateModelAsync(user);
                _context.Membres.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Membre/Edit/5
        [Authorize(Roles = "Administrateur")]
        public ActionResult Edit(string id)
        {
            MembreViewModel userVM = new MembreViewModel(_context.Membres.FirstOrDefault(u => u.Id == id));
            return View(userVM);
        }

        // POST: Membre/Edit/5
        [Authorize(Roles = "Administrateur")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MembreViewModel userVM)
        {
            try
            {
                ApplicationUser user = _context.Membres.Include(s => s.InfoSup).FirstOrDefault(u => u.Id == userVM.ID);
               if (userVM.Fichier != null)
               {
                   if (user.InfoSup.Image == null)
                       user.InfoSup.Image = new Image();
                   user.InfoSup.Image.Nom = userVM.Fichier.FileName;
                   user.InfoSup.Image.Type = userVM.Fichier.ContentType;
                   user.InfoSup.Image.Taille = (int)userVM.Fichier.Length;

                   user.InfoSup.Image.Data = new byte[user.InfoSup.Image.Taille];
                   var reader = userVM.Fichier.OpenReadStream();
                   reader.ReadAsync(user.InfoSup.Image.Data, 0, user.InfoSup.Image.Taille);
                   reader.Dispose();
               }
                TryUpdateModelAsync(user);
                user.InfoSup.Rang = userVM.Rang;
                user.UserName = userVM.NomUtilisateur;
                user.NormalizedUserName = userVM.NomUtilisateur.ToUpper();
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        // GET: Membre/Delete/5
        [Authorize(Roles = "Administrateur")]
        public ActionResult Delete(string id)
        {
            MembreViewModel userVM = new MembreViewModel(_context.Membres.FirstOrDefault(u => u.Id == id));

            return View(userVM);
        }

        // POST: Membre/Delete/5
        [Authorize(Roles = "Administrateur")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                _context.Users.Remove(_context.Users.FirstOrDefault(u => u.Id == id));
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}