using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreRestAPI.ViewModel
{
    public class RestaurantViewModel
    {
        public int Id { get; set; }
        public string OrignalName { get; set; }
        public string NameWithLocation { get; set; }
        public string Cusine { get; set; }
    }
}
