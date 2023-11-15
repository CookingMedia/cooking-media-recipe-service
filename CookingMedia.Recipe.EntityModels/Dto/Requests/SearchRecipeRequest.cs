using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingMedia.Recipe.EntityModels.Dto.Requests;

public class SearchRecipeRequest : PageRequest
{
    public string Name { get; set; } = null!;
    public int RecipeCategoryId { get; set; }
    public int RecipeStyleId { get; set; }
    public int CookingMethodId { get; set; }
}
