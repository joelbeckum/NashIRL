using System.Collections.Generic;

namespace NashIRL.Models.ViewModels
{
    public class EventDetailViewModel
    {
        public Event CurrentEvent { get; set; }

        public int CurrentUserProfileId { get; set; }

        public List<Comment> Comments { get; set; } 
    }
}
