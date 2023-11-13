using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookingMedia.Recipe.EntityModels;

namespace CookingMedia.Recipe.Repositories;
public class RecipeStepRepository : GenericRepository<RecipeStep>
{
    public RecipeStepRepository(CookingMediaRecipeDbContext context) : base(context)
    {
    }
}
