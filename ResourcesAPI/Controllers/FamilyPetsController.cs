using Microsoft.AspNetCore.Mvc;
using ResourcesAPI.Services;
using ResourcesAPI.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace ResourcesAPI.Controllers
{
    [Route("api/families/{familyId:Guid}/pets")]
    [ApiController]
    public class FamilyPetsController : ControllerBase
    {
        private readonly PetService _petService;

        public FamilyPetsController(PetService petService)
        {
            _petService = petService;
        }

        [HttpGet]
        public ActionResult<List<Pet>> Get([FromRoute] Guid familyId) =>
            _petService.Get(familyId);

        [HttpGet("{petId:Guid}", Name = "GetPet")]
        public ActionResult<Pet> Get([FromRoute] Guid familyId, Guid petId)
        {
            Pet foundPet = _petService.Get(familyId, petId);

            if (foundPet == null)
                return NotFound();

            return foundPet;
        }

        [HttpPost]
        public async Task<ActionResult<Pet>> Create([FromRoute] Guid familyId, Pet pet)
        {
            await _petService.Create(familyId, pet);

            return CreatedAtRoute("GetPet", new { familyId, petId = pet.Id }, pet);
        }

        [HttpPut("petId:Guid")]
        public async Task<ActionResult<Pet>> Update([FromRoute] Guid familyId, Guid petId, Pet petIn)
        {
            Pet updatedPet = await _petService.Update(familyId, petId, petIn);

            if (updatedPet == null)
                return NotFound();

            return updatedPet;
        }

        [HttpDelete("{petId:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid familyId, Guid petId)
        {
            Pet foundPet = _petService.Get(familyId, petId);

            if (foundPet == null)
                return NotFound();

            await _petService.Delete(familyId, foundPet);

            return NoContent();
        }
    }
}
