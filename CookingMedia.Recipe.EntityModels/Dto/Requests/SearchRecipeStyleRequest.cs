using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingMedia.Recipe.EntityModels.Dto.Requests;
public class SearchRecipeStyleRequest : PageRequest
{
    public string Name { get; set; } = string.Empty;
}
