using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelApp.Model.Orders;

namespace businesstravel.Controllers
{
    [Route("api/[controller]")]
    // [Authorize]
    public class OrdersController : Controller
    {
        // POST api/values
        [HttpPost]
        public ShopResponse Post([FromBody]ShopRequest request)
        {

            return new ShopResponse
            {
               
            };
        }
    }
}
