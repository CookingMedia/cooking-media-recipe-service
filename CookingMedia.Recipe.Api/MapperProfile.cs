using AutoMapper;
using CookingMedia.Recipe.Api.Model;
using CookingMedia.Recipe.EntityModels;
using CookingMedia.Recipe.EntityModels.LookUp;

namespace CookingMedia.Recipe.Api;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CookingMethod, CookingMethodModel>().ReverseMap();
        CreateMap<CookingMethod, UpdateCookingMethodModel>().ReverseMap();
        // TODO: mapp status

        CreateMap<EntityModels.Recipe, RecipeModel>().ReverseMap();
        CreateMap<AddRecipeRequest, EntityModels.Recipe>();
        CreateMap<AddRecipeStepRequest, RecipeStep>();
        CreateMap<AddRecipeAmountRequest, RecipeAmount>();
        CreateMap<RecipeStep, RecipeModel.Types.StepModel>();
        CreateMap<RecipeAmount, RecipeModel.Types.AmountModel>();
    }
}
