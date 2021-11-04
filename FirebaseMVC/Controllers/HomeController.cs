using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NashIRL.Models;
using NashIRL.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using NashIRL.Repositories;
using System.Linq;
using System;

namespace NashIRL.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IHobbyRepository _hobbyRepository;
        private readonly IEventRepository _eventRepository;

        public HomeController(IUserProfileRepository userProfileRepository, IHobbyRepository hobbyRepository, IEventRepository eventRepository)
        {
            _userProfileRepository = userProfileRepository;
            _hobbyRepository = hobbyRepository;
            _eventRepository = eventRepository;
        }

        public IActionResult Index()
        {
            var currentUserProfile = _userProfileRepository.GetById(GetCurrentUserProfileId());
            var hobbies = _hobbyRepository.GetAll().Where(h => h.IsApproved).ToList();
            var events = _eventRepository.GetAll().Where(e => e.EventOn > DateTime.Now).ToList();
            foreach (var e in events)
            {
                e.Name = TruncateString(e.Name, 30);
                e.Description = TruncateString(e.Description, 125);
            }

            var vm = new HomeViewModel()
            {
                UserProfile = currentUserProfile,
                Hobbies = hobbies,
                Events = events
            };

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
