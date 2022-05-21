namespace TTGServer.Models.ServiceModels
{
    public class StationModel
    {
        public string? Description { get; set; }
        public int Position { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Name { get; set; } = null!;
    }
}
