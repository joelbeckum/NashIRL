using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashIRL.Models.ViewModels
{
    public class HobbyIndexViewModel
    {
        public Hobby Hobby { get; set; }

        public List<Event> Events { get; set; }

        public int CurrentUserProfileId { get; set; }
    }
}
