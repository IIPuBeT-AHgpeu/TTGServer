using TTGServer.Models.DBModels;
using TTGServer.Models.ServiceModels;

namespace TTGServer.Services
{
    public class MapInfoService : IService
    {
        public TTG_ver3Context? Context { get; set; }
        public MapInfoService(TTG_ver3Context? context)
        {
            Context = context;
        }
        public IEnumerable<UnitMapInfo> GetUnitsMapInfo(string wayName)
        {
            if(Context != null)
            {
                try
                {
                    //Get wayId by wayName
                    int wayId = Context.Ways.First(way => way.Name == wayName).Id;

                    List<UnitMapInfo> unitMapInfos = new List<UnitMapInfo>();
                    List<Unit> units = Context.Units.Where(unit => unit.WayId == wayId).ToList();

                    foreach (var unit in units)
                    {
                        if (unit.Latitude != null && unit.Longitude != null)
                            unitMapInfos.Add((UnitMapInfo)unit);
                    }

                    return unitMapInfos;
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in searching of Unit.");
                    return new List<UnitMapInfo>();
                }
            }
            else
            {
                return new List<UnitMapInfo>();
            }
        }
        public void UpdateUnitMapInfo(UnitMapInfo unitMapInfo)
        {
            if(Context != null && unitMapInfo != null)
            {
                try
                {
                    //Get unit from DB by number
                    Unit unit = Context.Units.First(unit => unit.Number == unitMapInfo.Number);

                    unit += unitMapInfo;
                    Context.SaveChanges();
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in searching of Unit.");
                }
            }
        }
        public IEnumerable<StationMapInfo> GetStationsMapInfo(string wayName)
        {
            if(Context != null)
            {
                try
                {
                    //Get wayId by wayName
                    int wayId = Context.Ways.First(way => way.Name == wayName).Id;

                    List<StationMapInfo> stationMapInfos = new List<StationMapInfo>();
                    List<Station> stations = Context.Stations.Where(station => station.WayId == wayId).ToList();

                    foreach (var station in stations)
                    {
                        stationMapInfos.Add((StationMapInfo)station);
                    }

                    stationMapInfos.Sort((StationMapInfo a, StationMapInfo b) =>
                    {
                        return a.Position.CompareTo(b.Position);
                    });

                    return stationMapInfos;
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in searching of Station.");
                    return new List<StationMapInfo>();
                }
            }
            else
            {
                return new List<StationMapInfo>();
            }
        }

        //пассажир
    }
}
