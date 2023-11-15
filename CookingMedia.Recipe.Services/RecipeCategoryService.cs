using System.Linq.Expressions;
using CookingMedia.Recipe.EntityModels.Dto.Requests;
using CookingMedia.Recipe.EntityModels.Dto.Responses;
using CookingMedia.Recipe.EntityModels.LookUp;
using CookingMedia.Recipe.Repositories;

namespace CookingMedia.Recipe.Services;

public class RecipeCategoryService
{
    private readonly UnitOfWork _unitOfWork;

    public RecipeCategoryService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public PageResult<RecipeCategory> Get(SearchRecipeCategoryRequest request)
    {
        return _unitOfWork.RecipeCategories.Get(request,
            new Expression<Func<RecipeCategory, bool>>[] { c => c.Name.Contains(request.Name) },
            o => o.OrderBy(c => c.DisplayIndex));
    }

    public RecipeCategory? GetById(int id)
    {
        return _unitOfWork.RecipeCategories.GetById(id);
    }

    public RecipeCategory Add(RecipeCategory recipeCategory)
    {
        _unitOfWork.RecipeCategories.Insert(recipeCategory);
        _unitOfWork.Save();
        return recipeCategory;
    }

    public void Update(RecipeCategory recipeCategory)
    {
        _unitOfWork.RecipeCategories.Update(recipeCategory);
        _unitOfWork.Save();
    }

    public void Delete(int id)
    {
        _unitOfWork.RecipeCategories.Delete(id);
        _unitOfWork.Save();
    }
}