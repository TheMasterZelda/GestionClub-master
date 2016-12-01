using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestionClub.Data;
using GestionClub.Models.ForumViewModels;
using Microsoft.EntityFrameworkCore;
using GestionClub.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Sakura.AspNetCore;

namespace GestionClub.Controllers
{
    public class ForumController : Controller
    {
        private ApplicationDbContext _context = null;
        public ForumController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Forum
        public ActionResult Index(string ordretri = null, string motrecherche = null, string champrec = null, int page = 1)
        {
            List<ForumViewModel> liste_vm = new List<ForumViewModel>();

            var forums = _context.Forums
                         .Include(m => m.Messages)
                         .Include(u => u.User)
                         .OrderBy<Forum, object>(delegate (Forum f)
                         {
                             if (ordretri != null)
                             {
                                 if (ordretri == "Titre")
                                     return f.Titre;
                                 else if (ordretri == "NombreMessage")
                                     return f.NombreMessage;
                             }
                             return f.ID;
                         })
                         .Where<Forum>(delegate (Forum f)
                         {
                             if (motrecherche != null && champrec != null)
                             {
                                 if (champrec == "Titre")
                                     return f.Titre.ToUpper().Contains(motrecherche.ToUpper());
                                 if (champrec == "NombreMessage")
                                 {
                                     int iNombreMots = 0;
                                     if (Int32.TryParse(motrecherche, out iNombreMots))
                                     {
                                         return f.NombreMessage == iNombreMots;
                                     }
                                     return false;
                                 }

                             }
                             return true;
                         });

            foreach (Forum f in forums)
            {
                liste_vm.Add(new ForumViewModel(f));
            }
            if (HttpContext.Request.IsAjaxRequest())
                return PartialView("_IndexListeForumPartial", liste_vm.ToPagedList<ForumViewModel>(5, page));
            else
                return View(liste_vm.ToPagedList<ForumViewModel>(5, page));
        }

        // GET: Forum/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Forum/Create
        [Authorize(Roles = "Administrateur,Modérateur")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Forum/Create
        [Authorize(Roles = "Administrateur,Modérateur")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // var user = _context.Membres.Where(m => m.UserName == User.FindFirstValue(ClaimTypes.NameIdentifier)).FirstOrDefault();
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Forum forum = new Forum();
                forum.Auteur = User.Identity.Name;
                forum.DateCreation = DateTime.Now;
                forum.Dernier = User.Identity.Name;
                forum.DateModification = DateTime.Now;
                forum.User = _context.Membres.Where(m => m.Id == userId).SingleOrDefault();
                forum.UserId = userId;
                TryUpdateModelAsync(forum);
                _context.Forums.Add(forum);
                _context.SaveChanges();

                Message message = new Message()
                {
                    ForumID = forum.ID,
                    Auteur = User.Identity.Name,
                    DateMessage = DateTime.Now,
                    Texte = forum.Description,
                    User = _context.Membres.Where(m => m.Id == userId).SingleOrDefault(),
                    UserId = userId
                  
                };
                _context.Messages.Add(message);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Forum/Edit/5
        [Authorize(Roles = "Administrateur,Modérateur")]
        public ActionResult Edit(int id)
        {
            ForumViewModel forumVM = new ForumViewModel(_context.Forums.FirstOrDefault(f => f.ID == id));

            return View(forumVM);
        }

        // POST: Forum/Edit/5
        [Authorize(Roles = "Administrateur,Modérateur")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ForumViewModel modelForum)
        {
            try
            {
                Forum forum = _context.Forums.FirstOrDefault(f => f.ID == modelForum.ID);

                TryUpdateModelAsync(forum);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Forum/Delete/5
        [Authorize(Roles = "Administrateur,Modérateur")]
        public ActionResult Delete(int id)
        {
            ForumViewModel forumVM = new ForumViewModel(_context.Forums.FirstOrDefault(f => f.ID == id));

            return View(forumVM);
        }

        // POST: Forum/Delete/5
        [Authorize(Roles = "Administrateur,Modérateur")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
             //   foreach (Message message in Forums.ListeForum.SingleOrDefault(f => f.ID == id).Messages)
             //       _context.Messages.Remove(message);
                _context.Forums.Remove(_context.Forums.FirstOrDefault(f => f.ID == id));
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