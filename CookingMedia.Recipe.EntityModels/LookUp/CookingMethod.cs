using CookingMedia.Recipe.EntityModels.Enum;

namespace CookingMedia.Recipe.EntityModels.LookUp;

public class CookingMethod : GenericEntity
{
    public required string Name { get; set; }
    public CookingMethodStatus Status { get; set; }
}
