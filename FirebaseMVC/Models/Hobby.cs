using System;
using System.ComponentModel.DataAnnotations;

namespace NashIRL.Models
{
    public class Hobby
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsApproved { get; set; }

        public int ApprovedBy { get; set; }

        public DateTime? ApprovedOn { get; set; }
    }
}
