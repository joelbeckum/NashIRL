using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NashIRL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashIRL.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository;

        public EventController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        //// GET: EventController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: EventController/Details/5
        public ActionResult Details(int id)
        {
            var currentEvent = _eventRepository.GetById(id);
            if (currentEvent == null)
            {
                return NotFound();
            }
            return View(currentEvent);
        }

        // GET: EventController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EventController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EventController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EventController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EventController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
