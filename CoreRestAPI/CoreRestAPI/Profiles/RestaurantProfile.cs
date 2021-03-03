using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreRestAPI.Profiles
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            //Creating Mapping from Source type to Destination type
            CreateMap<DataModel.Restaurant, ViewModel.RestaurantViewModel>()
                .ForMember(
                    dest => dest.NameWithLocation,
                    opt => opt.MapFrom(src => $"{src.Name} from {src.Location}")
                )
                .ForMember(
                    dest => dest.OrignalName,
                    opt => opt.MapFrom(src => src.Name)
                )
                .ForMember(
                    dest => dest.Cusine,
                    opt => opt.MapFrom(src => $"Authentic {src.Cusine}")
                );
        }
    }
}
