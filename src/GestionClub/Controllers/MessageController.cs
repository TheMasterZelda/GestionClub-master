using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GestionClub.Models.ForumViewModels;
using GestionClub.Data;
using Microsoft.EntityFrameworkCore;
using GestionClub.Models;
using GestionClub.Models.MessageViewModels;
using System.Security.Claims;

namespace GestionClub.Controllers
{
    public class MessageController : Controller
    {
        private ApplicationDbContext _context = null;
        public MessageController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Message
        public ActionResult Index(int id)
        {
            List<MessageViewModel> liste_vm = new List<MessageViewModel>();

            var message = _context.Messages.Where(f => f.ForumID == id)
                                .Include(m => m.Frm)
                                .Include(u => u.User)
                                .Include(i => i.User.InfoSup)
                                .Include(i => i.User.InfoSup.Image);

            foreach (Message f in message)
            {
                liste_vm.Add(new MessageViewModel(f));
            }
            return View(liste_vm);
        }

        // GET: Message/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Message/Create
        [Authorize(Roles = "Administrateur,Modérateur,Utilisateur")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Message/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrateur,Modérateur,Utilisateur")]
        public ActionResult Create(IFormCollection collection, int id)
        {
            try
            {
                Message curMessage = _context.Messages.SingleOrDefault(m => m.ID == id);
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                Message message = new Message();

                TryUpdateModelAsync(message);
                message.ForumID = curMessage.ForumID;
                message.Auteur = User.Identity.Name;
                message.DateMessage = DateTime.Now;
                message.User = _context.Membres.Where(m => m.Id == userId).SingleOrDefault();
                message.UserId = userId;

                Message goodIdMessage = new Message()
                {
                    Auteur = message.Auteur,
                    DateMessage = message.DateMessage,
                    ForumID = message.ForumID,
                    Texte = message.Texte,
                    User = message.User,
                    UserId = message.UserId
                };


                _context.Messages.Add(goodIdMessage);
                _context.SaveChanges();
                _context.Forums.FirstOrDefault(f => f.ID == curMessage.ForumID).DateModification = DateTime.Now;
                _context.Forums.FirstOrDefault(f => f.ID == curMessage.ForumID).Dernier = User.Identity.Name;
                _context.SaveChanges();

                return RedirectToAction("Index", new { id = curMessage.ForumID });
            }
            catch
            {
                return View();
            }
        }

        // GET: Message/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Message/Edit/5
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

        // GET: Message/Delete/5
        [Authorize(Roles = "Administrateur,Modérateur")]
        public ActionResult Delete(int id)
        {
            MessageViewModel messageVM = new MessageViewModel(_context.Messages.FirstOrDefault(t => t.ID == id));

            return View(messageVM);
        }

        // POST: Message/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrateur,Modérateur")]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                int forumid = _context.Messages.SingleOrDefault(m => m.ID == id).ForumID;
                _context.Messages.Remove(_context.Messages.SingleOrDefault(m => m.ID == id));
                _context.SaveChanges();

                return RedirectToAction("Index", new { id = forumid });
            }
            catch
            {
                return View();
            }
        }
    }
}