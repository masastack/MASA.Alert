namespace Masa.Alert.Infrastructure.EntityFrameworkCore.Extensions;

public class PaginatedList<TEntity> where TEntity : class
{
    public long Total { get; set; }

    public int TotalPages { get; set; }

    public List<TEntity> Result { get; set; } = default!;
}
