namespace TTGServer.Models.ServiceModels
{
    public class OwnerPersonalInfoUpdate : OwnerPersonalInfo
    {
        public string OldLogin { get; set; }
        public string OldPassword { get; set; }
    }
}
