using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NashIRL.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using NashIRL.Repositories;

namespace NashIRL.Controllers
{
    [Authorize]
    public class HomeController : Controller
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
            var userProfileId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userProfile = _userProfileRepository.GetById(userProfileId);
            return View(userProfile);
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
