using TTGServer.Models.DBModels;

namespace TTGServer.Models.ServiceModels
{
    public class PassengerProfileInfo : IProfileInfo
    {
        public string? Name { get; set; }
        public string Login { get; set; }

        public static explicit operator PassengerProfileInfo(Passenger passenger)
        {
            return new PassengerProfileInfo()
            {
                Name = passenger.Name,
                Login = passenger.Login,
            };
        }
        public static Passenger operator +(Passenger passenger, PassengerProfileInfo passengerProfileInfo)
        {
            passenger.Name = passengerProfileInfo.Name;
            passenger.Login = passengerProfileInfo.Login;

            return passenger;
        }
    }
}
