using System.Collections.Generic;

namespace NashIRL.Models.ViewModels
{
    public class HobbyAdminViewModel
    {
        public List<Hobby> ApprovedHobbies { get; set; }

        public List<Hobby> PendingHobbies { get; set; }
    }
}
