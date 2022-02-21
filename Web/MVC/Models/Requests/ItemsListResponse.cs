namespace MVC.Models.Requests
{
    public class ItemsListResponse<T>
    {
        public long Count { get; init; }

        public IEnumerable<T> Data { get; init; } = null!;
    }
}
