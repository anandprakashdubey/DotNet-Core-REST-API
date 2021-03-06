using AutoMapper;
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
        private readonly IRestaurantData RestaurantService;
        private readonly IMapper Mapper;

        public Restaurant(IRestaurantData RestaurantService
            , IMapper Mapper)
        {
            this.RestaurantService = RestaurantService;
            this.Mapper = Mapper;
        }

        /// <summary>
        /// This method will return List of Restaurant.
        /// </summary>
        /// <returns>An ActionResult of Restaurant</returns>
        [HttpGet]
        //[HttpHead] Head is same as GET of API but it does not return any body, it is just to check connection or resource exists.
        public ActionResult<IEnumerable<RestaurantViewModel>> GetRestaurant()
        {
            var _data = RestaurantService.GetRestaurantByName(null).ToList();

            /* 
             * 
             * Below approach is not good for Converting object to different object.
             * Hence we need to use AutoMapper tool.
              
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

             * Below Line using Mapper to conver repo entity to model.
             *
             */

            return Ok(Mapper.Map<IEnumerable<RestaurantViewModel>>(_data));
        }

        /// <summary>
        /// This method filter out restaurants list with provided name, if parameter is null then it will return full list.
        /// </summary>
        /// <param name="restaurantName">This should be the restaurant name</param>
        /// <returns>An ActionResult of Restaurant</returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{restaurantName}", Name = "GetRestaurantByName")]
        public IActionResult GetRestaurant(string restaurantName)
        {
            var _data = RestaurantService.GetRestaurantByName(restaurantName).ToList();
            if (_data.Count == 0)
                return NotFound();

            return Ok(Mapper.Map<IEnumerable<RestaurantViewModel>>(_data));
        }

        [HttpGet("{cusineType}/restaurants")]
        public IActionResult GetRestaurantByCusine(string cusineType)
        {
            var _data = RestaurantService.GetRestaurantByCusine(cusineType).ToList();
            if (_data.Count == 0)
                return NotFound();

            return Ok(Mapper.Map<IEnumerable<RestaurantViewModel>>(_data));
        }

        [HttpGet("{cusineType}/restaurants/{dishName}")]
        public IActionResult GetRestaurantByNameAndCusine(string cusineType, string dishName)
        {
            //throw new Exception("Boom");  --> Example of exception handelling in prod, refer startup
            var _data = RestaurantService.GetRestaurantByNameAndCusine(cusineType, dishName).ToList();
            if (_data.Count == 0)
                return NotFound();

            return Ok(Mapper.Map<IEnumerable<RestaurantViewModel>>(_data));
        }

        [HttpPost]
        public IActionResult CreateRestaurant(DataModel.Restaurant _restaurant)
        {
            if (_restaurant == null)
                return BadRequest();

            var data = this.RestaurantService.AddRestaurant(_restaurant);

            return CreatedAtRoute("GetRestaurantByName", new { restaurantName = data.Name }, data);

        }
    }
}
