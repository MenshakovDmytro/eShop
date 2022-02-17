namespace Catalog.Host.Models.Requests
{
    public class UpdateTypeRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
