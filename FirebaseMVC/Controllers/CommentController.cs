using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NashIRL.Models;
using NashIRL.Models.ViewModels;
using NashIRL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NashIRL.Controllers
{
    [Authorize]
    public class CommentController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet("{id}")]
        public ActionResult Create(int id)
        {
            var vm = new CommentFormViewModel()
            {
                EventId = id
            };

            return View(vm);
        }

        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentFormViewModel vm, int id)
        {
            try
            {
                vm.Comment.UserProfileId = GetCurrentUserProfileId();
                vm.Comment.EventId = id;

                _commentRepository.Add(vm.Comment);

                return RedirectToAction("Details", "Event", new { id = id });
            }
            catch (Exception)
            {
                return View(vm);
            }
        }

        public ActionResult Edit(int id)
        {
            var comment = _commentRepository.GetById(id);

            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Comment comment)
        {
            try
            {
                _commentRepository.Update(comment);

                return RedirectToAction("Details", "Event", new { id = comment.EventId });
            }
            catch (Exception)
            {
                return View(comment);
            }
        }

        public ActionResult Delete(int id)
        {
            var comment = _commentRepository.GetById(id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            var comment = _commentRepository.GetById(id);

            try
            {
                var eventId = comment.EventId;
                _commentRepository.Delete(id);
                return RedirectToAction("Details", "Event", new { id = eventId });
            }
            catch
            {
                return View(comment);
            }
        }

        private int GetCurrentUserProfileId()
        {
            string idString = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return int.Parse(idString);
        }
    }
}
