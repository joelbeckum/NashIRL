using Microsoft.AspNetCore.Mvc;
using NashIRL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NashIRL.Components
{
    public class AdminOptionsViewComponent : ViewComponent
    {
        private IUserProfileRepository _userProfileRepository;

        public AdminOptionsViewComponent(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        //public IViewComponentResult Invoke()
        //{
        //    var currentUser = _userProfileRepository.GetById(GetCurrentUserProfileId());
        //}

        //private int GetCurrentUserProfileId()
        //{
        //    string idString = User.FindFirst(ClaimTypes.NameIdentifier).Value;

        //    return int.Parse(idString);
        //}
    }
}
