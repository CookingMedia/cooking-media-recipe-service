using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookingMedia.Recipe.EntityModels;
using CookingMedia.Recipe.Repositories;

namespace CookingMedia.Recipe.Services;
public class RecipeAmountService
{
    private readonly UnitOfWork _unitOfWork;

    public RecipeAmountService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public RecipeAmount? GetById(int id) => _unitOfWork.RecipeAmounts.GetById(id);

    public RecipeAmount Add(RecipeAmount recipeStep)
    {
        if (!_unitOfWork.Recipes.ExistById(recipeStep.RecipeId))
            throw new ArgumentException($"Recipe#{recipeStep.RecipeId} not found");
        _unitOfWork.RecipeAmounts.Insert(recipeStep);
        _unitOfWork.Save();
        return recipeStep;
    }

    public void Update(RecipeAmount recipeAmount)
    {
        _unitOfWork.RecipeAmounts.Update(recipeAmount);
        _unitOfWork.Save();
    }

    public void Delete(int id)
    {
        _unitOfWork.RecipeAmounts.Delete(id);
        _unitOfWork.Save();
    }
}
