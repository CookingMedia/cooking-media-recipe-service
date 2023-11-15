using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookingMedia.Recipe.EntityModels;
using CookingMedia.Recipe.EntityModels.Enum;
using CookingMedia.Recipe.EntityModels.LookUp;

namespace CookingMedia.Recipe.Repositories;

public class RecipeStyleRepository : GenericRepository<RecipeStyle>
{
    public RecipeStyleRepository(CookingMediaRecipeDbContext context) : base(context)
    {
    }

    public override void Delete(object id)
    {
        var recipeStyle = GetById(id);
        if (recipeStyle != null)
            recipeStyle.Status = RecipeStyleStatus.Disable;
    }
}