using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResourcesAPI.Models;
using ResourcesAPI.Services;

namespace ResourcesAPI.Controllers
{
    [Route("api/families/{familyId:Guid}/adults")]
    [ApiController]
    public class FamilyAdultsController : ControllerBase
    {
        private readonly AdultService _adultService;

        public FamilyAdultsController(AdultService adultService)
        {
            _adultService = adultService;
        }

        [HttpGet]
        public ActionResult<List<Adult>> Get([FromRoute] Guid familyId) =>
            _adultService.Get(familyId);

        [HttpGet("{adultId:Guid}", Name = "GetAdult")]
        public ActionResult<Adult> Get([FromRoute] Guid familyId, Guid adultId)
        {
            Adult foundAdult = _adultService.Get(familyId, adultId);

            if (foundAdult == null)
                return NotFound();

            return foundAdult;
        }

        [HttpPost]
        public async Task<ActionResult<Adult>> Create([FromRoute] Guid familyId, Adult adult)
        {
            await _adultService.Create(familyId, adult);

            return CreatedAtRoute("GetAdult", new { familyId, adultId = adult.Id }, adult);
        }

        [HttpPut("{adultId:Guid}")]
        public async Task<ActionResult<Adult>> Update([FromRoute] Guid familyId, Guid adultId, Adult adultIn)
        {
            Adult updatedAdult = await _adultService.Update(familyId, adultId, adultIn);

            if (updatedAdult == null)
                return NotFound();

            return updatedAdult;
        }

        [HttpDelete("{adultId:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid familyId, Guid adultId)
        {
            Adult foundAdult = _adultService.Get(familyId, adultId);

            if (foundAdult == null)
                return NotFound();

            await _adultService.Delete(familyId, foundAdult);

            return NoContent();
        }
    }
}
