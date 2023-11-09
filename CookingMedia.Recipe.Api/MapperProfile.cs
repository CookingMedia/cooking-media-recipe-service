using AutoMapper;
using CookingMedia.Recipe.EntityModels.LookUp;

namespace CookingMedia.Recipe.Api;

public class MapperProfile : Profile
{
    protected MapperProfile()
    {
        CreateMap<CookingMethod, CookingMethodModel>().ReverseMap();
        CreateMap<CookingMethod, UpdateCookingMethodModel>().ReverseMap();
        // TODO: mapp status
    }
}
