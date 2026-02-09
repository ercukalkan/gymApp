namespace GymApp.Shared.Pagination;

public class Pagination<T>(int pageNumber, int pageSize, int count, IQueryable<T> source) where T : class
{
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
    public int Count { get; set; } = count;
    public IQueryable<T> Source { get; set; } = source;
}