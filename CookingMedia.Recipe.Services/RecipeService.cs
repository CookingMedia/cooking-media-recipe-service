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
}
