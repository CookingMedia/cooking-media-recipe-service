using System.Linq.Expressions;
using CookingMedia.Recipe.Repositories;

namespace CookingMedia.Recipe.Services;

public class RecipeService
{
    private readonly UnitOfWork _unitOfWork;

    public RecipeService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public EntityModels.Recipe Add(EntityModels.Recipe recipe)
    {
        _unitOfWork.Recipes.Insert(recipe);
        _unitOfWork.Save();
        return recipe;
    }

    public IEnumerable<EntityModels.Recipe> Get(IEnumerable<Expression<Func<EntityModels.Recipe, bool>>>? filter = null)
    {
        return _unitOfWork.Recipes.Get(filter, order => order.OrderByDescending(r => r.Id),
            $"{nameof(EntityModels.Recipe.RecipeCategory)}," +
            $"{nameof(EntityModels.Recipe.RecipeStyle)}," +
            $"{nameof(EntityModels.Recipe.CookingMethod)}");
    }

    public EntityModels.Recipe? GetById(int id)
    {
        return _unitOfWork.Recipes.Get(new Expression<Func<EntityModels.Recipe, bool>>[] { recipe => recipe.Id == id },
                null,
                $"{nameof(EntityModels.Recipe.RecipeCategory)},{nameof(EntityModels.Recipe.RecipeStyle)}," +
                $"{nameof(EntityModels.Recipe.CookingMethod)},{nameof(EntityModels.Recipe.Amounts)}," +
                $"{nameof(EntityModels.Recipe.Steps)}")
            .FirstOrDefault();
    }

    public void Update(EntityModels.Recipe recipe)
    {
        _unitOfWork.Recipes.Update(recipe);
        _unitOfWork.Save();
    }

    public void Delete(int id)
    {
        _unitOfWork.Recipes.Delete(id);
        _unitOfWork.Save();
    }
}