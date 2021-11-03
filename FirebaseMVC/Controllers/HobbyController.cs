using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

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

        public ActionResult Delete(int id)
        {
            return View();
        }

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
