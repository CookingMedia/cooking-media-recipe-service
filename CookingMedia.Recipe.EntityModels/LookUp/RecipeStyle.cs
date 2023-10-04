using CookingMedia.Recipe.EntityModels.Enum;

namespace CookingMedia.Recipe.EntityModels.LookUp;

public class RecipeStyle : GenericEntity
{
    public required string Name { get; set; }
    public RecipeStyleStatus Status { get; set; }
}
