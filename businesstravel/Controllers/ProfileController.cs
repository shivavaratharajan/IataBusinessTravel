using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelApp.Model;
using TravelApp.Model.Orders;

namespace businesstravel.Controllers
{
    [Route("api/[controller]")]
   // // [Authorize]
    public class ProfileController : Controller
    {
        // GET api/values/5
        [HttpGet("{profileId}")]
        public IActionResult Get(int profileId)
        {
           var profile =  DataLayer.Client.DataLoader.GetProfile(profileId);

            if(profile == null )
            {
                return NotFound();
            }
            return Ok(profile);
        }

        [HttpGet("CompanyPolicy/{companyId}")]
        public IActionResult GetCompanyPolicy(int companyId)
        {
            var companyPolicy = DataLayer.Client.DataLoader.GetCompanyPolicy(companyId);

            if (companyPolicy == null)
            {
                return NotFound();
            }
            return Ok(companyPolicy);
        }

        [HttpGet("Company/{companyId}")]
        public IActionResult GetCompany(int companyId)
        {
            var company= DataLayer.Client.DataLoader.GetCompany(companyId);

            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        // POST api/values
        [HttpPost]
        // [Authorize]
        public ShopResponse Post([FromBody]ShopRequest request)
        {

            return new ShopResponse
            {

            };
        }
    }
    }
