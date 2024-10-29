namespace Reado.Domain.Request;

public  abstract class PageRequest : Request
{
    public int PageNumber { get; set; } = Configuration.DefaultPageNumber;
    public int PageSize { get; set; } = Configuration.DefaultPageSize;
}   
