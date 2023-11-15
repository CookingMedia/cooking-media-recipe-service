namespace CookingMedia.Recipe.EntityModels.Dto.Responses;

public class PageResult<T>
{
    public IList<T> Result { get; set; } = new List<T>();
    public int Total { get; set; }
    public int PageCount { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
}
