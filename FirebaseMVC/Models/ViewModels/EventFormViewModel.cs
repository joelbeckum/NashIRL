using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace NashIRL.Models.ViewModels
{
    public class EventFormViewModel
    {
        public Event NewEvent { get; set; }

        public List<Hobby> Hobbies { get; set; }

        public int HobbyNavId { get; set; }

        public IFormFile Image { get; set; }
    }
}
