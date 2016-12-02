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
                Participant p = new Participant();
                p.DateInscription = DateTime.Now;
                p.NomUtilisateur = User.Identity.Name;
                _context.Participants.Add(p);
                _context.SaveChanges();



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

        // POST : Tournoi/Promote/
        /// [HttpPost]
        [Authorize(Roles = "Administrateur,Modérateur")]
        public ActionResult Promote(int id, int partie, int participant)
        {
            try
            {
                Tournoi tournoi = _context.Tournois
                            .Include(u => u.Parties)
                            .Include(p => p.Participants)
                            .FirstOrDefault(t => t.ID == id);
                Partie pppp = _context.Parties.FirstOrDefault(t => t.ID == partie);
                Participant papapapa = _context.Participants.FirstOrDefault(t => t.ID == participant);
                Participant User1 = _context.Participants.FirstOrDefault(t => t.ID == pppp.UserId1);
                Participant User2 = _context.Participants.FirstOrDefault(t => t.ID == pppp.UserId2);

                if (User1 == papapapa)
                    User2.Etat = false;
                else
                    User1.Etat = false;
                pppp.Gagnant = true;
                pppp.DateJouer = DateTime.Now;


                switch (pppp.Numero)
                {
                    case 1:
                        tournoi.Parties.Where(n => n.Numero == 3).FirstOrDefault().UserId1 = papapapa.ID;
                        tournoi.Parties.Where(n => n.Numero == 3).FirstOrDefault().User1 = papapapa;
                        break;
                    case 2:
                        tournoi.Parties.Where(n => n.Numero == 3).FirstOrDefault().UserId2 = papapapa.ID;
                        tournoi.Parties.Where(n => n.Numero == 3).FirstOrDefault().User2 = papapapa;
                        break;
                    case 3:
                        tournoi.Parties.Where(n => n.Numero == 7).FirstOrDefault().UserId1 = papapapa.ID;
                        tournoi.Parties.Where(n => n.Numero == 7).FirstOrDefault().User1 = papapapa;
                        break;
                    case 4:
                        tournoi.Parties.Where(n => n.Numero == 6).FirstOrDefault().UserId1 = papapapa.ID;
                        tournoi.Parties.Where(n => n.Numero == 6).FirstOrDefault().User1 = papapapa;
                        break;
                    case 5:
                        tournoi.Parties.Where(n => n.Numero == 6).FirstOrDefault().UserId2 = papapapa.ID;
                        tournoi.Parties.Where(n => n.Numero == 6).FirstOrDefault().User2 = papapapa;
                        break;
                    case 6:
                        tournoi.Parties.Where(n => n.Numero == 7).FirstOrDefault().UserId2 = papapapa.ID;
                        tournoi.Parties.Where(n => n.Numero == 7).FirstOrDefault().User2 = papapapa;
                        break;
                    case 7:
                        //tournoi.Parties[2].UserId1 = participant.ID;
                        break;
                }
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET : Tournoi/Commencer/
        public ActionResult Commencer()
        {
            return View();
        }
        // POST: Tournoi/Commencer/5
        [HttpPost]
        [Authorize(Roles = "Administrateur,Modérateur")]
        public ActionResult Commencer(TournoiViewModel model)
        {
            try
            {
                Tournoi tournoi = _context.Tournois.FirstOrDefault(t => t.ID == model.ID);
                int nbr = tournoi.Participants.Count;
                switch (nbr)
                {
                    case 8:
                        tournoi.NombrePartie = 7;
                        break;
                    case 4:
                        tournoi.NombrePartie = 6;
                        break;
                    case 2:
                        tournoi.NombrePartie = 5;
                        break;
                    default:
                        return View();
                }
                int current = 0;
                for (int i = 0; i < tournoi.NombrePartie; i++)
                {
                    if (i < tournoi.Participants.Count / 2)
                    {
                        Partie partie = new Partie()
                        {
                            Numero = i + 1,
                            Etat = false,
                            TournoiId = tournoi.ID,
                            UserId1 = tournoi.Participants[++current].ID,
                            UserId2 = tournoi.Participants[++current].ID
                        };
                        _context.Parties.Add(partie);
                        _context.SaveChanges();
                    }
                    else
                    {
                        Partie partie = new Partie()
                        {
                            Numero = i + 1,
                            Etat = false,
                            TournoiId = tournoi.ID
                        };
                        _context.Parties.Add(partie);
                        _context.SaveChanges();
                    }
                }

                tournoi.Start = true;
                _context.SaveChanges();
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