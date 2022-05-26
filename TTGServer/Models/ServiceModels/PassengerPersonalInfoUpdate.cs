namespace TTGServer.Models.ServiceModels
{
    public class PassengerPersonalInfoUpdate : PassengerPersonalInfo
    {
        public string OldLogin { get; set; }
        public string OldPassword { get; set; }
    }
}
