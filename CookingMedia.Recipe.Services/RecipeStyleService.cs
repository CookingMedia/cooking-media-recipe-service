using System.Linq.Expressions;
using CookingMedia.Recipe.EntityModels.Dto.Requests;
using CookingMedia.Recipe.EntityModels.Dto.Responses;
using CookingMedia.Recipe.EntityModels.LookUp;
using CookingMedia.Recipe.Repositories;

namespace CookingMedia.Recipe.Services;

public class RecipeStyleService
{
    private readonly UnitOfWork _unitOfWork;

    public RecipeStyleService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public PageResult<RecipeStyle> Get(SearchRecipeStyleRequest request)
    {
        return _unitOfWork.RecipeStyles.Get(request,
            new Expression<Func<RecipeStyle, bool>>[] { c => c.Name.Contains(request.Name) },
            o => o.OrderBy(c => c.Name));
    }

    public RecipeStyle? GetById(int id)
    {
        return _unitOfWork.RecipeStyles.GetById(id);
    }

    public RecipeStyle Add(RecipeStyle recipeCategory)
    {
        _unitOfWork.RecipeStyles.Insert(recipeCategory);
        _unitOfWork.Save();
        return recipeCategory;
    }

    public void Update(RecipeStyle recipeCategory)
    {
        _unitOfWork.RecipeStyles.Update(recipeCategory);
        _unitOfWork.Save();
    }

    public void Delete(int id)
    {
        _unitOfWork.RecipeStyles.Delete(id);
        _unitOfWork.Save();
    }
}