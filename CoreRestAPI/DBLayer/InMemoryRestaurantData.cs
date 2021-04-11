using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBLayer
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurtants;
        public InMemoryRestaurantData()
        {
            restaurtants = new List<Restaurant>()
            {
                new Restaurant  { Id = 1, Name = "Idli Sambhar", Cusine = CusineType.Indian, Location = "Indian" },
                new Restaurant  { Id = 2, Name = "Sphagetti", Cusine = CusineType.Italian , Location = "Irish"},
                new Restaurant  { Id = 3, Name = "Tacos", Cusine = CusineType.Mexican , Location = "Mexican"},
                new Restaurant  { Id = 4, Name = "Poha Jalebi", Cusine = CusineType.Indian, Location = "Indian" },
                new Restaurant  { Id = 5, Name = "Chhola Bhatura", Cusine = CusineType.Indian, Location = "Indian" },
                new Restaurant  { Id = 6, Name = "Ham Burger", Cusine = CusineType.American , Location = "American"},
                new Restaurant  { Id = 7, Name = "Samosa", Cusine = CusineType.Indian , Location = "Indian"},
            };
        }
        public IEnumerable<Restaurant> GetRestaurantByName(string name)
        {
            return from r in restaurtants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Id
                   select r;
        }

        public Restaurant GetRestaurantById(int Id)
        {
            return (from r in restaurtants
                    where r.Id.Equals(Id)
                    select r).FirstOrDefault();
        }

        public Restaurant UpdateRestaurant(Restaurant res)
        {
            var ob = restaurtants.Where(item => item.Id == res.Id).FirstOrDefault();
            if (ob != null)
            {
                restaurtants.Remove(ob);
                restaurtants.Add(res);
            }
            return res;
        }

        public Restaurant AddRestaurant(Restaurant res)
        {
            var id = restaurtants.Max(item => item.Id);
            res.Id = id + 1;
            restaurtants.Add(res);

            return res;
        }

        public Restaurant DeleteRestaurant(int Id)
        {
            var _res = restaurtants.SingleOrDefault(item => item.Id == Id);
            restaurtants.Remove(_res);
            return _res;
        }

        public IEnumerable<Restaurant> GetRestaurantByCusine(string cusineType)
        {
            return from r in restaurtants
                   where string.IsNullOrEmpty(cusineType) || r.Cusine.ToString() == cusineType
                   orderby r.Id
                   select r;
        }

        public IEnumerable<Restaurant> GetRestaurantByNameAndCusine(string cusineType, string Name)
        {
            return from r in restaurtants
                   where string.IsNullOrEmpty(cusineType) || r.Cusine.ToString() == cusineType
                   && string.IsNullOrEmpty(Name) || r.Name.StartsWith(Name)
                   orderby r.Id
                   select r;
        }

    }
}
