namespace Catalog.Host.Models.Requests
{
    public class GetByFieldRequest<T>
    {
        public T Field { get; set; } = default(T) !;
    }
}
