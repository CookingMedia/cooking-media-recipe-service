using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookingMedia.Recipe.EntityModels;
using CookingMedia.Recipe.Repositories;

namespace CookingMedia.Recipe.Services;
public class RecipeStepService
{
    private readonly UnitOfWork _unitOfWork;

    public RecipeStepService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public RecipeStep? GetById(int id) => _unitOfWork.RecipeSteps.GetById(id);

    public RecipeStep Add(RecipeStep recipeStep)
    {
        if (!_unitOfWork.Recipes.ExistById(recipeStep.RecipeId))
            throw new ArgumentException($"Recipe#{recipeStep.RecipeId} not found");
        _unitOfWork.RecipeSteps.Insert(recipeStep);
        _unitOfWork.Save();
        return recipeStep;
    }

    public void Update(RecipeStep recipeStep)
    {
        _unitOfWork.RecipeSteps.Update(recipeStep);
        _unitOfWork.Save();
    }

    public void Delete(int id)
    {
        _unitOfWork.RecipeSteps.Delete(id);
        _unitOfWork.Save();
    }
}
