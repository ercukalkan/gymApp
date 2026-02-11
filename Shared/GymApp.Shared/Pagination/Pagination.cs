namespace GymApp.Shared.Pagination;

public class Pagination<T>(int pageNumber, int pageSize, int total, IReadOnlyList<T> source) where T : class
{
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
    public int Total { get; set; } = total;
    public IReadOnlyList<T> Source { get; set; } = source;
}