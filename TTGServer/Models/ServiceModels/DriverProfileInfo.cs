using TTGServer.Models.DBModels;

namespace TTGServer.Models.ServiceModels
{
    public class DriverProfileInfo : IProfileInfo
    {
        public string? Name { get; set; }
        public string Login { get; set; }
        public string? Model { get; set; }
        public string Number { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Passport { get; set; } = null!;

        public static explicit operator DriverProfileInfo(Unit driver)
        {
            return new DriverProfileInfo()
            {
                Name = driver.Name,
                Login = driver.Login,
                Model = driver.Model,
                Number = driver.Number,
                Status = driver.Status,
                Passport = driver.Passport,
            };
        }
        public static Unit operator +(Unit unit, DriverProfileInfo driverProfileInfo)
        {
            unit.Name = driverProfileInfo.Name;
            unit.Model = driverProfileInfo.Model;
            unit.Number = driverProfileInfo.Number;
            unit.Status = driverProfileInfo.Status;
            unit.Passport = driverProfileInfo.Passport;

            return unit;
        }
    }
}
