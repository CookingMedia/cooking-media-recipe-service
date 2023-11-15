using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookingMedia.Recipe.EntityModels;
using CookingMedia.Recipe.EntityModels.Enum;
using CookingMedia.Recipe.EntityModels.LookUp;

namespace CookingMedia.Recipe.Repositories;

public class RecipeCategoryRepository : GenericRepository<RecipeCategory>
{
    public RecipeCategoryRepository(CookingMediaRecipeDbContext context) : base(context)
    {
    }

    public override void Delete(object id)
    {
        var recipeCategory = GetById(id);
        if (recipeCategory == null) return;
        recipeCategory.Status = RecipeCategoryStatus.Disable;
    }
}