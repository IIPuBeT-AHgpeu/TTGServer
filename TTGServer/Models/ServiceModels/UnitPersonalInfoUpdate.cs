namespace TTGServer.Models.ServiceModels
{
    public class UnitPersonalInfoUpdate : UnitPersonalInfo
    {
        public string OldLogin { get; set; }
        public string OldPassword { get; set; }
    }
}
