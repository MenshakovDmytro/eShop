namespace Catalog.Host.Models.Requests
{
    public class UpdateBrandRequest
    {
        public string OldName { get; set; } = null!;
        public string NewName { get; set; } = null!;
    }
}
