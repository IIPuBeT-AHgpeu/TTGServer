namespace TTGServer.Models.ServiceModels
{
    public interface IRegistationModel : IProfileInfo
    {
        public string Password { get; set; }
    }
}
