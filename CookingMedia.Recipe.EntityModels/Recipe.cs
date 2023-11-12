using System.ComponentModel.DataAnnotations.Schema;
using CookingMedia.Recipe.EntityModels.Enum;
using CookingMedia.Recipe.EntityModels.LookUp;

namespace CookingMedia.Recipe.EntityModels;

public class Recipe : GenericEntity
{
    [ForeignKey(nameof(RecipeCategory))]
    public int RecipeCategoryId { get; set; }

    [ForeignKey(nameof(RecipeStyle))]
    public int RecipeStyleId { get; set; }

    [ForeignKey(nameof(CookingMethod))]
    public int CookingMethodId { get; set; }

    public string Name { get; set; } = null!;
    public string? ImageLink { get; set; }
    public int CookingTime { get; set; }
    public string? Description { get; set; }
    public int ServingSize { get; set; }
    public IList<RecipeStep> Steps { get; set; } = new List<RecipeStep>();
    public IList<RecipeAmount> Amounts { get; set; } = new List<RecipeAmount>();
    public RecipeStatus Status { get; set; }

    public RecipeCategory? RecipeCategory { get; set; }
    public RecipeStyle? RecipeStyle { get; set; }
    public CookingMethod? CookingMethod { get; set; }
}
