using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashIRL.Models.ViewModels
{
    public class EventFormViewModel
    {
        public Event newEvent { get; set; }

        public List<Hobby> Hobbies { get; set; }
    }
}
