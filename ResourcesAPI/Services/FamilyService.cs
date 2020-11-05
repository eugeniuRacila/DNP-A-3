using ResourcesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesAPI.Services
{
    public class FamilyService
    {
        private readonly PersistenceService _persistenceService;
        private List<Family> _families { get; set; }
        public List<Family> Families
        {
            get
            {
                return new List<Family>(_families);
            }
        }
        public static readonly string _familiesPath = "families.json";

        public FamilyService(PersistenceService persistenceService)
        {
            _persistenceService = persistenceService;
            _families = persistenceService.InitializeData<Family>();
        }

        public List<Family> Get() =>
            Families;

        public Family Get(Guid id) =>
            _families.FirstOrDefault(family => family.Id == id);

        public async Task<Family> Create(Family family)
        {
            _families.Add(family);
            await SaveData();

            return family;
        }

        public async Task<Family> Update(Guid id, Family familyIn)
        {
            Family foundFamily = _families.First(family => family.Id == id);

            foundFamily.Update(familyIn);
            await SaveData();

            return foundFamily;
        }

        public async Task Delete(Guid id)
        {
            _families.Remove(Get(id));
            await SaveData();
        }

        public async Task SaveData()
        {
            await _persistenceService.WriteList(Families);
        }
    }
}
