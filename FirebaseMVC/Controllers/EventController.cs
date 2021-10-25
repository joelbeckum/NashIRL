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
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly IHobbyRepository _hobbyRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public EventController(IEventRepository eventRepository, IHobbyRepository hobbyRepository, IUserProfileRepository userProfileRepository)
        {
            _eventRepository = eventRepository;
            _hobbyRepository = hobbyRepository;
            _userProfileRepository = userProfileRepository;
        }

        //// GET: EventController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Details(int id)
        {
            var currentEvent = _eventRepository.GetById(id);
            var currentUserProfileId = GetCurrentUserProfileId();
            if (currentEvent == null)
            {
                return NotFound();
            }
            var vm = new EventDetailViewModel()
            {
                CurrentEvent = currentEvent,
                CurrentUserProfileId = currentUserProfileId
            };
            return View(vm);
        }

        public ActionResult Create()
        {
            var vm = new EventFormViewModel();
            vm.Hobbies = _hobbyRepository.GetAll();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventFormViewModel vm)
        {
            try
            {
                vm.NewEvent.UserProfileId = GetCurrentUserProfileId();

                _eventRepository.Add(vm.NewEvent);

                return RedirectToAction("Details", "Hobby", new { id = vm.NewEvent.HobbyId });
            }
            catch (Exception)
            {
                return View(vm);
            }
        }

        public ActionResult Edit(int id)
        {
            var vm = new EventFormViewModel()
            {
                NewEvent = _eventRepository.GetById(id),
                Hobbies = _hobbyRepository.GetAll()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EventFormViewModel vm)
        {
            try
            {
                _eventRepository.Update(vm.NewEvent);
                return RedirectToAction("Details", new { id = vm.NewEvent.Id });
            }
            catch
            {
                return View(vm);
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

        private int GetCurrentUserProfileId()
        {
            string idString = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return int.Parse(idString);
        }
    }
}
