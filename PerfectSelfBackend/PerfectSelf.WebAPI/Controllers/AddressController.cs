using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerfectSelf.WebAPI.Models;
using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using PerfectSelf.WebAPI.Common;

namespace PerfectSelf.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        public AddressController()
        {
        }

        [Route("countries")]
        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            return Ok(Global.countryMap);
        }

        [Route("states/{countryCode}")]
        [HttpGet]
        public async Task<IActionResult> GetStates(String countryCode)
        {
            return Ok(Global.stateMap[countryCode]);
        }

        [Route("cities/{stateCode}")]
        [HttpGet]
        public async Task<IActionResult> GetCities(String stateCode)
        {
            return Ok(Global.cityMap[stateCode]);
        }
    }
}
