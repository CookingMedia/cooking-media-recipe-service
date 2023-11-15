using AutoMapper;
using CookingMedia.Recipe.Api.Model;
using CookingMedia.Recipe.EntityModels;
using CookingMedia.Recipe.EntityModels.Dto.Responses;
using CookingMedia.Recipe.EntityModels.LookUp;

namespace CookingMedia.Recipe.Api;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap(typeof(PageResult<>), typeof(PagingResultModel));
        CreateMap<SearchRecipeRequest, EntityModels.Dto.Requests.SearchRecipeRequest>();
        CreateMap<SearchRecipeCategoryRequest, EntityModels.Dto.Requests.SearchRecipeCategoryRequest>();
        CreateMap<SearchRecipeStyleRequest, EntityModels.Dto.Requests.SearchRecipeStyleRequest>();
        CreateMap<SearchCookingMethodModel, EntityModels.Dto.Requests.SearchCookingMethodRequest>();

        CreateMap<CookingMethod, CookingMethodModel>().ReverseMap();
        CreateMap<CookingMethod, UpdateCookingMethodModel>().ReverseMap();
        // TODO: map status

        CreateMap<EntityModels.Recipe, RecipeModel>();
        CreateMap<RecipeCategory, RecipeCategoryModel>();
        CreateMap<RecipeStyle, RecipeStyleModel>();
        CreateMap<RecipeStep, StepModel>();
        CreateMap<RecipeAmount, AmountModel>();
        CreateMap<AddRecipeRequest, EntityModels.Recipe>();
        CreateMap<AddRecipeRequest.Types.AddRecipeStepRequest, RecipeStep>();
        CreateMap<AddRecipeRequest.Types.AddRecipeAmountRequest, RecipeAmount>();
        CreateMap<UpdateRecipeRequest, EntityModels.Recipe>();
        CreateMap<AddRecipeAmountRequest, RecipeAmount>();
        CreateMap<UpdateRecipeAmountRequest, RecipeAmount>();
        CreateMap<AddRecipeStepRequest, RecipeStep>();
        CreateMap<UpdateRecipeStepRequest, RecipeStep>();

        CreateMap<AddRecipeStyleRequest, RecipeStyle>();
        CreateMap<UpdateRecipeStyleRequest, RecipeStyle>();
        CreateMap<AddRecipeCategoryRequest, RecipeCategory>();
        CreateMap<UpdateRecipeCategoryRequest, RecipeCategory>();
    }
}