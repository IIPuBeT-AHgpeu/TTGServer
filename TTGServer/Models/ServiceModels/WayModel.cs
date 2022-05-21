using TTGServer.Models.DBModels;

namespace TTGServer.Models.ServiceModels
{
    public class WayModel
    {
        public string OwnerLogin { get; set; } = null!;
        public float Price { get; set; }
        public string Name { get; set; } = null!;
        public float Rent { get; set; }
    }
}
