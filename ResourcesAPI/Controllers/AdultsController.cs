using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ResourcesAPI.Models;
using ResourcesAPI.Services;

namespace ResourcesAPI.Controllers
{
    [Route("api/adults")]
    [ApiController]
    public class AdultsController : ControllerBase
    {
        private readonly FamilyService _familyService;

        private List<Adult> _adults = new List<Adult>
        {
            new Adult()
            {
                Age = 31,
                EyeColor = "Brown",
                FirstName = "Mike",
                HairColor = "Blue",
                Height = 180,
                JobTitle = Adult.JobTitles.Captain,
                LastName = "Brownie",
                Sex = "Male",
                Weight = 69.420f
            }
        };

        public AdultsController(FamilyService familyService)
        {
            _familyService = familyService;
        }

        [HttpGet]
        public ActionResult<List<Adult>> Get() =>
            Ok(_adults);

        [HttpGet("{adultId:Guid}")]
        public ActionResult Get(Guid adultId) =>
            Ok(adultId.ToString());

    }
}
