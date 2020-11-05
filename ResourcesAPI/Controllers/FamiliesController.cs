using Microsoft.AspNetCore.Mvc;
using ResourcesAPI.Models;
using ResourcesAPI.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResourcesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamiliesController : ControllerBase
    {
        private readonly FamilyService _familyService;

        public FamiliesController(FamilyService familyService)
        {
            _familyService = familyService;
        }

        [HttpGet]
        public ActionResult<List<Family>> Get() =>
            _familyService.Get();

        [HttpGet("{id:Guid}", Name = "GetFamily")]
        public ActionResult<Family> Get(Guid id)
        {
            Family foundFamily = _familyService.Get(id);

            if (foundFamily == null)
                return NotFound();

            return foundFamily;
        }

        [HttpPost]
        public async Task<ActionResult<Family>> Create(Family family)
        {
            await _familyService.Create(family);

            return family;
        }

        // Change to Patch
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<Family>> Update(Guid id, Family familyIn)
        {
            Family foundFamily = _familyService.Get(id);

            if (foundFamily == null)
                return NotFound();

            Family updatedFamily = await _familyService.Update(id, familyIn);

            return updatedFamily;
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Console.WriteLine("delete" + System.Text.Json.JsonSerializer.Serialize(_familyService.Get()));
            Family foundFamily = _familyService.Get(id);

            if (foundFamily == null)
                return NotFound();

            await _familyService.Delete(id);

            return NoContent();
        }
    }
}
