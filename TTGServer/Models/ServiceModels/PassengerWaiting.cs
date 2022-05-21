namespace TTGServer.Models.ServiceModels
{
    public class PassengerWaiting
    {
        public float StationLatitude { get; set; }
        public float StationLongitude { get; set; }
        public string Login { get; set; }
        public string WayName { get; set; }
        public bool IsSet { get; set; }
    }
}
