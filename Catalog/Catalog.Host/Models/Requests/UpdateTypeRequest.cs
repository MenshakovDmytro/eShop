namespace Catalog.Host.Models.Requests
{
    public class UpdateTypeRequest
    {
        public string OldName { get; set; } = null!;
        public string NewName { get; set; } = null!;
    }
}
