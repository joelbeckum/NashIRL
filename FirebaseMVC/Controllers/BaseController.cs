using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NashIRL.Controllers
{
    public abstract class BaseController : Controller
    {
        protected int GetCurrentUserProfileId()
        {
            string idString = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return int.Parse(idString);
        }
    }
}
