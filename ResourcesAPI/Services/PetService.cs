using ResourcesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesAPI.Services
{
    public class PetService
    {
        private readonly FamilyService _familyService;

        public PetService(FamilyService familyService)
        {
            _familyService = familyService;
        }

        public List<Pet> Get(Guid familyId) =>
            _familyService.Get(familyId)?.Pets;

        public Pet Get(Guid familyId, Guid petId) =>
            _familyService.Get(familyId)?
                .Pets.FirstOrDefault(pet => pet.Id == petId);

        public List<Pet> GetChildPets(Guid familyId, Guid childId) =>
            _familyService.Get(familyId)?
                .Children.FirstOrDefault(child => child.Id == childId)?
                .Pets;

        public Pet GetChildPet(Guid familyId, Guid childId, Guid petId) =>
            _familyService.Get(familyId)?
                .Children.FirstOrDefault(child => child.Id == childId)?
                .Pets.FirstOrDefault(pet => pet.Id == petId);

        public async Task Create(Guid familyId, Pet pet)
        {
            _familyService.Get(familyId)?.Pets.Add(pet);
            await _familyService.SaveData();
        }

        public async Task CreateChildPet(Guid familyId, Guid childId, Pet pet)
        {
            _familyService.Get(familyId)?
                .Children.FirstOrDefault(child => child.Id == childId)?
                .AddPet(pet);
            await _familyService.SaveData();
        }

        public async Task<Pet> Update(Guid familyId, Guid petId, Pet petIn)
        {
            Pet foundPet = Get(familyId, petId);

            if (foundPet == null)
                return null;

            foundPet.Update(petIn);
            await _familyService.SaveData();

            return foundPet;
        }

        public async Task<Pet> UpdateChildPet(Guid familyId, Guid childId, Guid petId, Pet petIn)
        {
            Pet foundPet = GetChildPet(familyId, childId, petId);

            if (foundPet == null)
                return null;

            foundPet.Update(petIn);
            await _familyService.SaveData();

            return foundPet;
        }

        public async Task Delete(Guid familyId, Pet pet)
        {
            _familyService.Get(familyId)?.Pets.Remove(pet);
            await _familyService.SaveData();
        }

        public async Task DeleteChildPet(Guid familyId, Guid childId, Pet pet)
        {
            _familyService.Get(familyId)?
                .Children.FirstOrDefault(child => child.Id == childId)?
                .Pets.Remove(pet);
            await _familyService.SaveData();
        }
    }
}
