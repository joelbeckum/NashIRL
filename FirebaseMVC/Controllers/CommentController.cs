using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NashIRL.Models.ViewModels;
using NashIRL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashIRL.Controllers
{
    public class CommentController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public ActionResult RenderComments(int eventId)
        {
            var comments = _commentRepository.GetByEvent(eventId);
            var vm = new CommentsViewModel()
            {
                Comments = comments
            };

            return PartialView("_Comments", vm);
        }

        //        // GET: CommentController
        //        public ActionResult Index()
        //        {
        //            return View();
        //        }

        //        // GET: CommentController/Details/5
        //        public ActionResult Details(int id)
        //        {
        //            return View();
        //        }

        //        // GET: CommentController/Create
        //        public ActionResult Create()
        //        {
        //            return View();
        //        }

        //        // POST: CommentController/Create
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult Create(IFormCollection collection)
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
    }
}
