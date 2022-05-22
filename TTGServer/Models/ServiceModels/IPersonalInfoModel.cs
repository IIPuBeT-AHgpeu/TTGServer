namespace TTGServer.Models.ServiceModels
{
    public interface IPersonalInfoModel : IProfileInfo
    {
        public string Password { get; set; }
    }
}
