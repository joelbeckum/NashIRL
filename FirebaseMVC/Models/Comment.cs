using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashIRL.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Body { get; set; }

        public int UserProfileId { get; set; }

        public UserProfile UserProfile { get; set; }

        public int EventId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
