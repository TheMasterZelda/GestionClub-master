using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestionClub.Data;
using Microsoft.EntityFrameworkCore;
using GestionClub.Models.TournoiViewModels;
using GestionClub.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GestionClub.Controllers
{
    public class TournoiController : Controller
    {
        private ApplicationDbContext _context = null;
        public TournoiController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Tournoi
        public ActionResult Index()
        {
            List<TournoiViewModel> liste_vm = new List<TournoiViewModel>();

            var tournois = _context.Tournois
                         .Include(p => p.Participants)
                         .Include(u => u.Parties);

            foreach (Tournoi t in tournois)
            {
                liste_vm.Add(new TournoiViewModel(t));
            }

            liste_vm.OrderBy(d => d.DateCreation);

            return View(liste_vm);
        }

        // GET: Tournoi/Details/5
        public ActionResult Details(int id)
        {
            TournoiViewModel tournoiVM = new TournoiViewModel(_context.Tournois.Include(p => p.Participants).Include(p => p.Parties).FirstOrDefault(t => t.ID == id));

            return View(tournoiVM);
        }

        // GET : Tournoi/Arborescence/5
        public ActionResult Arborescence(int id)
        {
            TournoiViewModel tournoiVM = new TournoiViewModel(_context.Tournois.Include(p => p.Participants).Include(p => p.Parties).FirstOrDefault(t => t.ID == id));

            return View(tournoiVM);
        }

        // GET : Tournoi/Inscription/
        public ActionResult Inscription()
        {
            return View();
        }

        // POST: Tournoi/Inscription
        public ActionResult Inscription(IFormCollection collection)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Participant p = new Participant();
                p.DateInscription = DateTime.Now;
                p.NomUtilisateur = User.Identity.Name;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Tournoi/Create
        [Authorize(Roles = "Administrateur,Modérateur")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tournoi/Create
        [Authorize(Roles = "Administrateur,Modérateur")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Tournoi tournoi = new Tournoi();
                tournoi.Createur = User.Identity.Name;
                tournoi.Start = false;
                tournoi.State = false;
                tournoi.Participants = new List<Participant>();
                tournoi.Parties = new List<Partie>();

                TryUpdateModelAsync(tournoi);
                _context.Tournois.Add(tournoi);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tournoi/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tournoi/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tournoi/Delete/5
        [Authorize(Roles = "Administrateur,Modérateur")]
        public ActionResult Delete(int id)
        {
            TournoiViewModel tournoiVM = new TournoiViewModel(_context.Tournois.FirstOrDefault(f => f.ID == id));

            return View(tournoiVM);
        }

        // POST: Tournoi/Delete/5
        [Authorize(Roles = "Administrateur,Modérateur")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _context.Tournois.Remove(_context.Tournois.FirstOrDefault(f => f.ID == id));
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