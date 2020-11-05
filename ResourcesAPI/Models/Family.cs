using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResourcesAPI.Models
{
    public class Family
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string CreatedById { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public int HouseNumber { get; set; }
        public List<Adult> Adults { get; set; } = new List<Adult>();
        public List<Child> Children { get; set; } = new List<Child>();
        public List<Pet> Pets { get; set; } = new List<Pet>();

        public void Update(Family family)
        {
            StreetName = family.StreetName;
            HouseNumber = family.HouseNumber;
        }
    }
}
