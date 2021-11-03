using System.Collections.Generic;

namespace NashIRL.Models.ViewModels
{
    public class HomeViewModel
    {
        public UserProfile UserProfile { get; set; }

        public List<Hobby> Hobbies { get; set; }

        public List<Event> Events { get; set; }
    }
}
