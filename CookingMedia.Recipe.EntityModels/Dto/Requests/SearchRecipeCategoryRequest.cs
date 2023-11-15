using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingMedia.Recipe.EntityModels.Dto.Requests;
public class SearchRecipeCategoryRequest : PageRequest
{
    public string Name { get; set; } = string.Empty;
}
