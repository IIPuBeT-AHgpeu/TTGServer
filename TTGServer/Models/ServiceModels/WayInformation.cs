using TTGServer.Models.ServiceModels;

namespace TTGServer.Models.ServiceModels
{
    public class WayInformation
    {
        public float Price { get; set; }
        public float? AvrTripTime { get; set; }
        public IEnumerable<StationMapInfo> Stations { get; set; }
        public IEnumerable<UnitMapInfo> Cars { get; set; }
    }
}
