namespace Notex.Api.Models;

public class GetPaginatedListModel
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 15;
}