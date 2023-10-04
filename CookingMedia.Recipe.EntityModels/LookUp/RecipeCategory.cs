using CookingMedia.Recipe.EntityModels.Enum;

namespace CookingMedia.Recipe.EntityModels.LookUp;

public class RecipeCategory : GenericEntity
{
    public required string Name { get; set; }
    public required int DisplayIndex { get; set; }
    public RecipeCategoryStatus Status { get; set; }
}
