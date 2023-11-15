namespace CookingMedia.Recipe.EntityModels.Dto.Requests;

public class PageRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 3;
    public string OrderBy { get; set; } = string.Empty;
    public Direction Direction { get; set; } = Direction.Asc;

    public void Deconstruct(out int page, out int pageSize, out string sort)
    {
        page = Page < 1 ? 1 : Page;
        pageSize = PageSize < 1 ? 3 : PageSize;
        sort = OrderBy;
    }

    public void Deconstruct(
        out int page,
        out int pageSize,
        out string sort,
        out Direction direction
    )
    {
        page = Page < 1 ? 1 : Page;
        pageSize = PageSize < 1 ? 3 : PageSize;
        sort = OrderBy;
        direction = Direction;
    }
}

public enum Direction
{
    Asc,
    Desc,
}
