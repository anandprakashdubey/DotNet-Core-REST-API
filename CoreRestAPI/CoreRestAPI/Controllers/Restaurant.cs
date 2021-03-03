using CoreRestAPI.ViewModel;
using DBLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public ActionResult<IEnumerable<RestaurantViewModel>> GetRestaurant()
        {
            var _data = RestaurantService.GetRestaurantByName(null).ToList();

            /* 
             * 
             * Below approach is not good for Converting object to different object.
             * Hence we need to use AutoMapper tool.
             * 
             */
            var vm = new List<RestaurantViewModel>();
            foreach (var item in _data)
            {
                vm.Add(new RestaurantViewModel
                {
                    Id = item.Id,
                    OrignalName = item.Name,
                    NameWithLocation = $"{item.Name} from {item.Location}",
                    Cusine = $"Authentic {item.Cusine}"
                });
            }
            return Ok(vm);
        }

        [HttpGet("{restaurantName}")]
        public IActionResult GetRestaurant(string restaurantName)
        {
            var _data = RestaurantService.GetRestaurantByName(restaurantName).ToList();
            if (_data.Count == 0)
                return NotFound();

            var vm = new List<RestaurantViewModel>();
            foreach (var item in _data)
            {
                vm.Add(new RestaurantViewModel
                {
                    Id = item.Id,
                    OrignalName = item.Name,
                    NameWithLocation = $"{item.Name} from {item.Location}",
                    Cusine = $"Authentic {item.Cusine}"
                });
            }
            return Ok(vm);
        }
    }
}
