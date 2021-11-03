using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NashIRL.Models;
using NashIRL.Models.ViewModels;
using NashIRL.Repositories;
using System;
using System.Linq;

namespace NashIRL.Controllers
{
    [Authorize]
    public class HobbyController : BaseController
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

        public ActionResult Index()
        {
            var currentUser = _userProfileRepository.GetById(GetCurrentUserProfileId());

            if (currentUser.UserTypeId != 1)
            {
                return Unauthorized();
            }

            var hobbies = _hobbyRepository.GetAll();

            var vm = new HobbyAdminViewModel()
            {
                ApprovedHobbies = hobbies.Where(h => h.IsApproved).ToList(),
                PendingHobbies = hobbies.Where(h => !h.IsApproved).ToList()
            };

            return View(vm);
        }

        public ActionResult Details(int id)
        {
            var hobby = _hobbyRepository.GetById(id);
            if (!hobby.IsApproved)
            {
                return Unauthorized();
            }

            var events = _eventRepository.GetByHobbyId(id).Where(e => e.EventOn > DateTime.Now).ToList(); ;
            var currentUserProfileId = GetCurrentUserProfileId();

            var vm = new HobbyIndexViewModel()
            {
                Hobby = hobby,
                Events = events,
                CurrentUserProfileId = currentUserProfileId
            };

            return View(vm);
        }

        public ActionResult Create()
        {
            return View();
        }

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

        public ActionResult Edit(int id)
        {
            var currentUser = _userProfileRepository.GetById(GetCurrentUserProfileId());
            if (currentUser.UserTypeId != 1)
            {
                return Unauthorized();
            }

            var hobby = _hobbyRepository.GetById(id);

            return View(hobby);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Hobby hobby)
        {
            try
            {
                _hobbyRepository.Update(hobby);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(hobby);
            }
        }

        public ActionResult Delete(int id)
        {
            var hobby = _hobbyRepository.GetById(id);
            if (hobby == null)
            {
                return NotFound();
            }

            return View(hobby);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Hobby hobby)
        {
            try
            {
                _hobbyRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(hobby);
            }
        }
    }
}
