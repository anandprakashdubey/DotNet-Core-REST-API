using AutoMapper;
using DBLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreRestAPI.Controllers
{
    [Produces("application/json")]
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DishesController : ControllerBase
    {
        private readonly IRestaurantData RestaurantService;
        private readonly IMapper Mapper;

        public DishesController(IRestaurantData RestaurantService
            , IMapper Mapper)
        {
            this.RestaurantService = RestaurantService;
            this.Mapper = Mapper;
        }

        /// <summary>
        /// Create Restaurant, This is part of version 2 API.
        /// </summary>
        /// <param name="_restaurant"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateRestaurant(DataModel.Restaurant _restaurant)
        {
            if (_restaurant == null)
                return BadRequest();

            var data = this.RestaurantService.AddRestaurant(_restaurant);

            return CreatedAtRoute("GetRestaurantByName", new { restaurantName = data.Name }, data);

        }

        /// <summary>
        /// Return hardcoded values for version
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDishesList()
        {
            var data = new List<string>();
            data.Add("1.0");
            data.Add("2.0");
            data.Add("3.0");
            return Ok(data);
        }
    }
}
