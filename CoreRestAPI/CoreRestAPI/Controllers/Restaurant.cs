using DBLayer;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Restaurant : ControllerBase
    {
        private IRestaurantData RestaurantService { get; set; }
        public Restaurant(IRestaurantData RestaurantService)
        {
            this.RestaurantService = RestaurantService;
        }

        [HttpGet]
        public IActionResult GetRestaurant()
        {
            return Ok(RestaurantService.GetRestaurantByName(null));
        }

        [HttpGet("{restaurantName}")]
        public IActionResult GetRestaurant(string restaurantName)
        {
            var _data = RestaurantService.GetRestaurantByName(restaurantName).ToList();
            if (_data.Count == 0)
                return NotFound();

            return Ok(_data);
        }
    }
}
