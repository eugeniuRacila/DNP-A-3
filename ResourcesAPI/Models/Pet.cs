using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace ResourcesAPI.Models
{
    public class Pet
    {
        public enum PetSpecies
        {
            Cat,
            Dog,
            Turtle,
            Parrot,
            DrunkFriend,
        }

        [Key]
        public Guid Id { get; } = Guid.NewGuid();
        [Required]
        public PetSpecies Specie { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public void Update(Pet toUpdate)
        {
            Specie = toUpdate.Specie;
            Name = toUpdate.Name;
            Age = toUpdate.Age;
        }
    }
}
