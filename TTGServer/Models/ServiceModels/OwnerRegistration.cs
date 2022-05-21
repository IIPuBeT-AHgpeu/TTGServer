using TTGServer.Models.DBModels;

namespace TTGServer.Models.ServiceModels
{
    public class OwnerRegistration : IRegistationModel
    {
        public string Password { get; set; }
        public string? Name { get; set; }
        public string Login { get; set; }
        public string? License { get; set; }
        public Owner TransformToOwner()
        {
            return new Owner()
            {
                Password = this.Password,
                Login = this.Login,
                Name = this.Name,
                License = this.License
            };
        }
    }
}
