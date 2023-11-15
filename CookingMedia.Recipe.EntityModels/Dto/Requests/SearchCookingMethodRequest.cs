namespace CookingMedia.Recipe.EntityModels.Dto.Requests;

public class SearchCookingMethodRequest : PageRequest
{
    public string Name { get; set; } = string.Empty;
}