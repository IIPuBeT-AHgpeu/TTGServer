using TTGServer.Models.DBModels;
using TTGServer.Models.ServiceModels;

namespace TTGServer.Services
{
    public class AuthorizationService : IService
    {
        public TTG_ver3Context? Context { get; set; }
        public AuthorizationService(TTG_ver3Context? context)
        {
            Context = context;
        }
        public void RegisterUnit(UnitRegistration unitRegistration)
        {
            try
            {
                Way way = Context.Ways.First(way => way.Name == unitRegistration.WayName);
                string trueLogin = Context.Owners.First(owner => owner.Id == way.OwnerId).Login;

                if(trueLogin == unitRegistration.OwnerLogin)
                {
                    Unit newUnit = unitRegistration.TransformToUnit();
                    newUnit.WayId = way.Id;

                    Context.Units.Add(newUnit);
                    Context.SaveChanges();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Bad unit registration.");
            }
        }
        public void RegisterOwner(OwnerRegistration ownerRegistration)
        {
            try
            {
                Owner newOwner = ownerRegistration.TransformToOwner();

                Context.Owners.Add(newOwner);
                Context.SaveChanges();
            }
            catch (Exception)
            {
                Console.WriteLine("Bad owner registration.");
            }
        }
        public void RegisterPassenger(PassengerRegistration passengerRegistration)
        {
            try
            {
                Passenger newPassenger = passengerRegistration.TransformToPassenger();

                Context.Passengers.Add(newPassenger);
                Context.SaveChanges();
            }
            catch (Exception)
            {
                Console.WriteLine("Bad passenger registration.");   
            }
        }
    }
}
