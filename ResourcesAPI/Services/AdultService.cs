using ResourcesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesAPI.Services
{
    public class AdultService
    {
        private readonly FamilyService _familyService;

        public AdultService(FamilyService familyService)
        {
            _familyService = familyService;
        }

        public List<Adult> Get()
        {
            List<Adult> adults = new List<Adult>();

            // addrange

            return adults;
        }

        public List<Adult> Get(Guid familyId) =>
            _familyService.Get(familyId)?.Adults;

        public Adult Get(Guid familyId, Guid adultId) =>
            _familyService.Get(familyId)?.Adults.FirstOrDefault(adult => adult.Id == adultId);

        public async Task Create(Guid familyId, Adult adult)
        {
            _familyService.Get(familyId)?.Adults.Add(adult);
            await _familyService.SaveData();
        }

        public async Task<Adult> Update(Guid familyId, Guid adultId, Adult adultIn)
        {
            Adult foundAdult = Get(familyId, adultId);

            if (foundAdult == null)
                return null;

            foundAdult.Update(adultIn);
            await _familyService.SaveData();

            return foundAdult;
        }

        public async Task Delete(Guid familyId, Adult adult)
        {
            _familyService.Get(familyId)?.Adults.Remove(adult);
            await _familyService.SaveData();
        }
    }
}
