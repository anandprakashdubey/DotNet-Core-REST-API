using AutoMapper;
using CoreRestAPI.Model;
using CoreRestAPI.ViewModel;
using DBLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoreRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Record : ControllerBase
    {
        private readonly IRestaurantData RestaurantService;
        private readonly IMapper Mapper;
        public Record(IRestaurantData RestaurantService
            , IMapper Mapper)
        {
            this.RestaurantService = RestaurantService;
            this.Mapper = Mapper;
        }


        [HttpGet]
        public IActionResult GetRecord([FromQuery] PagingParameterModel pagingparametermodel)
        {
            var _data = RestaurantService.GetRestaurantByName(null).ToList();
            var finalData = Mapper.Map<IEnumerable<RestaurantViewModel>>(_data);

            int count = finalData.Count();
            int currentPage = pagingparametermodel.pageNumber;
            int pageSize = pagingparametermodel.pageSize;
            int totalCount = count;
            int totalPages = (int)Math.Ceiling(count / (double)pageSize);

            var items = finalData.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            var previousPage = currentPage > 1 ? "Yes" : "No";
            var nextPage = currentPage < totalPages ? "Yes" : "No";

            var paginationMetadata = new
            {
                totalCount = totalCount,
                pageSize = pageSize,
                currentPage = currentPage,
                totalPages = totalPages,
                previousPage,
                nextPage
            };
                       
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));
            return Ok(items);
        }
    }
}
