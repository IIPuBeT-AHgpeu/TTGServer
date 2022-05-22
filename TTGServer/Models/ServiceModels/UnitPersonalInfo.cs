using TTGServer.Models.DBModels;

namespace TTGServer.Models.ServiceModels
{
    public class UnitPersonalInfo : IPersonalInfoModel
    {
        public string Password { get; set; }
        public string? Name { get; set; }
        public string Login { get; set; }
        public string? Model { get; set; }
        public string Number { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Passport { get; set; } = null!;
        public string OwnerLogin { get; set; }
        public string WayName { get; set; }

        public Unit TransformToUnit()
        {
            return new Unit()
            {
                Model = this.Model,
                Latitude = null,
                Longitude = null,
                IsFull = false,
                Number = this.Number,
                Status = this.Status,
                Passport = this.Passport,
                Login = this.Login,
                Password = this.Password,
                Name = this.Name
            };
        }
    }
}
