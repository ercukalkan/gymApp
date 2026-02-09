namespace GymApp.Shared.Pagination;

public class PaginationParams
{
    public int MaxPageSize { get; } = 50;

    private int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }

    public int PageNumber { get; set; } = 1;

    public string? Sort { get; set; }
}