using DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBLayer
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantByName(string name);
        Restaurant GetRestaurantById(int Id);
        Restaurant UpdateRestaurant(Restaurant res);
        Restaurant AddRestaurant(Restaurant res);
        Restaurant DeleteRestaurant(int Id);
    }
}
