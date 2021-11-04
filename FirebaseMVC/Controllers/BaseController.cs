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
        
        public static string TruncateString(string inputString, int maxLength)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                return inputString;
            }

            return inputString.Length <= maxLength ? inputString : inputString.Substring(0, maxLength) + "...";
        }
    }
}
