using System.ComponentModel.DataAnnotations.Schema;

namespace CookingMedia.Recipe.EntityModels;

public class RecipeAmount : GenericEntity
{
    [ForeignKey(nameof(Recipe))]
    public int RecipeId { get; set; }
    public int IngredientId { get; set; }
    public required string Amount { get; set; }
    public Recipe? Recipe { get; set; }
}
