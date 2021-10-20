using System;
using System.ComponentModel.DataAnnotations;

namespace NashIRL.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public DateTime EventOn { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public int UserProfileId { get; set; }

        public UserProfile UserProfile { get; set; }

        [Required]
        public int HobbyId { get; set; }

        public Hobby Hobby { get; set; }
    }
}
