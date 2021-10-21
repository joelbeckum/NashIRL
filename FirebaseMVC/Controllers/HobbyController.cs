using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NashIRL.Models;
using NashIRL.Models.ViewModels;
using NashIRL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashIRL.Controllers
{
    [Authorize]
    public class HobbyController : Controller
    {
        private readonly IHobbyRepository _hobbyRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public HobbyController(IHobbyRepository hobbyRepository, IEventRepository eventRepository, IUserProfileRepository userProfileRepository)
        {
            _hobbyRepository = hobbyRepository;
            _eventRepository = eventRepository;
            _userProfileRepository = userProfileRepository;
        }

        // GET: HobbyController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: HobbyController/Details/5
        public ActionResult Details(int id)
        {
            var hobby = _hobbyRepository.GetById(id);
            var events = _eventRepository.GetByHobbyId(id);

            var vm = new HobbyIndexViewModel()
            {
                Hobby = hobby,
                Events = events
            };

            return View(vm);
        }

        // GET: HobbyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HobbyController/Create
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

        // GET: HobbyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HobbyController/Edit/5
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

        // GET: HobbyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HobbyController/Delete/5
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
