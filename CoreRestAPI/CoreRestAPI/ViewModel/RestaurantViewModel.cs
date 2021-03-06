using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreRestAPI.ViewModel
{
#pragma warning disable CS1591
    public class RestaurantViewModel
    {
        public int Id { get; set; }
        public string OrignalName { get; set; }
        public string NameWithLocation { get; set; }
        public string Cusine { get; set; }
    }
#pragma warning restore CS1591
}
