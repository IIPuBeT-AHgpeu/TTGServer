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
        public void RegisterUnit(UnitPersonalInfo unitRegistration)
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
        public void RegisterOwner(OwnerPersonalInfo ownerRegistration)
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
        public void RegisterPassenger(PassengerPersonalInfo passengerRegistration)
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
        public void UpdatePassenger(PassengerPersonalInfoUpdate passengerPersonalInfo)
        {
            try
            {
                Passenger passenger = Context.Passengers.First(pass => pass.Login == passengerPersonalInfo.OldLogin);

                passenger.Login = passengerPersonalInfo.Login;
                passenger.Name = passengerPersonalInfo.Name;
                passenger.Password = passengerPersonalInfo.Password;
                passenger.StationId = null;

                Context.SaveChanges();
            }
            catch (Exception)
            {
                Console.WriteLine("Error in update passenger info.");
            }
        }
        public void UpdateOwner(OwnerPersonalInfoUpdate ownerPersonalInfo)
        {
            try
            {
                Owner owner = Context.Owners.First(own => own.Login == ownerPersonalInfo.Login);

                owner.Login = ownerPersonalInfo.Login;
                owner.License = ownerPersonalInfo.License;
                owner.Password = ownerPersonalInfo.Password;
                owner.Name = ownerPersonalInfo.Name;

                Context.SaveChanges();
            }
            catch (Exception)
            {
                Console.WriteLine("Error in update owner info.");
            }
        }
        public void UpdateUnit(UnitPersonalInfoUpdate unitPersonalInfo)
        {
            try
            {
                Unit unit = Context.Units.First(unt => unt.Login == unitPersonalInfo.OldLogin);

                unit.Login = unitPersonalInfo.Login;
                unit.Password = unitPersonalInfo.Password;
                unit.Passport = unitPersonalInfo.Passport;
                unit.Name = unitPersonalInfo.Name;
                unit.Model = unitPersonalInfo.Model;
                unit.Latitude = null;
                unit.Longitude = null;
                unit.IsFull = false;
                unit.Status = unitPersonalInfo.Status;
                unit.Number = unitPersonalInfo.Number;

                int wayId = Context.Ways.First(way => way.Name == unitPersonalInfo.WayName).Id;

                unit.WayId = wayId;

                Context.SaveChanges();
            }
            catch (Exception)
            {
                Console.WriteLine("Error in update unit info.");
            }
        }
    }
}
