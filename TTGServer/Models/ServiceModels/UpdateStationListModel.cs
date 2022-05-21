namespace TTGServer.Models.ServiceModels
{
    public class UpdateStationListModel
    {
        public string OwnerLogin { get; set; }
        public string WayName { get; set; }
        public List<StationModel> Stations { get; set; }
    }
}
