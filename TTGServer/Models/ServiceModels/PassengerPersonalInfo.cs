using TTGServer.Models.DBModels;

namespace TTGServer.Models.ServiceModels
{
    public class PassengerPersonalInfo : IPersonalInfoModel
    {
        public string Password { get; set; }
        public string? Name { get; set; }
        public string Login { get; set; }
        public Passenger TransformToPassenger()
        {
            return new Passenger()
            {
                Login = this.Login,
                Name = this.Name,
                Password = this.Password,
                StationId = null
            };
        }
    }
}
