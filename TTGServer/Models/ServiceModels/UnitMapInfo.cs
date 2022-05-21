using TTGServer.Models.DBModels;

namespace TTGServer.Models.ServiceModels
{
    public class UnitMapInfo
    {
        public string Number { get; set; } = null!;
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public bool IsFull { get; set; }

        static public explicit operator UnitMapInfo(Unit unit)
        {
            return new UnitMapInfo()
            {
                Number = unit.Number,
                Latitude = unit.Latitude,
                Longitude = unit.Longitude,
                IsFull = unit.IsFull,
            };
        }
        static public Unit operator +(Unit unit, UnitMapInfo unitMapInfo)
        {
            unit.Latitude = unitMapInfo.Latitude;
            unit.Longitude = unitMapInfo.Longitude;
            unit.IsFull = unitMapInfo.IsFull;

            return unit;
        }
    }
}
