namespace AbuInt.Domain.Configuration;

public class PaginationParams
{
    private const int maxSize = 10;
    private int pageSize;

    public int PageSize
    {
        get => pageSize;
        set => pageSize = value > maxSize ? maxSize : value;
    }

    public int PageIndex { get; set; }
}