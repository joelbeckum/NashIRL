using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

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

        //        // GET: CommentController/Edit/5
        //        public ActionResult Edit(int id)
        //        {
        //            return View();
        //        }

        //        // POST: CommentController/Edit/5
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult Edit(int id, IFormCollection collection)
        //        {
        //            try
        //            {
        //                return RedirectToAction(nameof(Index));
        //            }
        //            catch
        //            {
        //                return View();
        //            }
        //        }

        //        // GET: CommentController/Delete/5
        //        public ActionResult Delete(int id)
        //        {
        //            return View();
        //        }

        //        // POST: CommentController/Delete/5
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult Delete(int id, IFormCollection collection)
        //        {
        //            try
        //            {
        //                return RedirectToAction(nameof(Index));
        //            }
        //            catch
        //            {
        //                return View();
        //            }
        //        }
        private int GetCurrentUserProfileId()
        {
            string idString = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return int.Parse(idString);
        }
    }
}
