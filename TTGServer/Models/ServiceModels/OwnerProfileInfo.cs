using TTGServer.Models.DBModels;

namespace TTGServer.Models.ServiceModels
{
    public class OwnerProfileInfo : IProfileInfo
    {
        public string? Name { get; set; }
        public string Login { get; set; }
        public string? License { get; set; }

        public static explicit operator OwnerProfileInfo(Owner owner)
        {
            return new OwnerProfileInfo()
            {
                Name = owner.Name,
                Login = owner.Login,
                License = owner.License
            };
        }
        public static Owner operator +(Owner owner, OwnerProfileInfo ownerProfileInfo)
        {
            owner.Name = ownerProfileInfo.Name;
            owner.Login = ownerProfileInfo.Login;
            owner.License = ownerProfileInfo.License;

            return owner;
        }
    }
}
