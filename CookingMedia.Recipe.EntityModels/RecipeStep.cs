using System.ComponentModel.DataAnnotations.Schema;

namespace CookingMedia.Recipe.EntityModels;

public class RecipeStep : GenericEntity
{
    [ForeignKey(nameof(Recipe))]
    public int RecipeId { get; set; }

    public required int Index { get; set; }
    public string? MediaURl { get; set; }
    public string? Description { get; set; }

    public Recipe? Recipe { get; set; }
}
