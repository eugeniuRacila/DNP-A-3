using System.Collections.Generic;
using System.Text.Json;

namespace ResourcesAPI.Models
{
    public class Child : Person
    {
        public enum ChildInterests
        {
            Football,
            VideoGames,
            Puzzles,
            Drawing,
            Singing,
            Dancing,
        }

        public HashSet<ChildInterests> Interests { get; set; } = new HashSet<ChildInterests>();
        
        public List<Pet> Pets { get; set; } = new List<Pet>();

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public void Update(Child toUpdate)
        {
            base.Update(toUpdate);
            Interests = toUpdate.Interests;
            Pets = toUpdate.Pets;
        }

        public void AddPet(Pet pet)
        {
            Pets.Add(pet);
        }

        public void Remove(Pet pet)
        {
            Pets.Remove(pet);
        }
    }
}
