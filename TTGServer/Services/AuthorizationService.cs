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

        public IProfileInfo? GetProfileInfo(char category, string login, string password)
        {
            category = category.ToString().ToUpper()[0];

            switch (category)
            {
                case 'P':
                    {
                        Passenger passenger;
                        try
                        {
                            passenger = Context.Passengers.First(pass => pass.Login == login);

                            if (passenger.Password == password)
                            {
                                return (PassengerProfileInfo)passenger;
                            }
                            else throw new Exception();
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Cant find passenger in DB.");
                            return null;
                        }
                    }
                case 'O':
                    {
                        Owner owner;
                        try
                        {
                            owner = Context.Owners.First(own => own.Login == login);

                            if (owner.Password == password)
                            {
                                return (OwnerProfileInfo)owner;
                            }
                            else throw new Exception();
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Cant find owner in DB.");
                            return null;
                        }
                    }
                case 'D':
                    {
                        Unit unit;
                        try
                        {
                            unit = Context.Units.First(unt => unt.Login == login);

                            if (unit.Password == password)
                            {
                                return (DriverProfileInfo)unit;
                            }
                            else throw new Exception();
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Cant find driver in DB.");
                            return null;
                        }
                    }
                default:
                    {
                        return null;
                    }
            }
        }
        public void UpdatePassenger(PassengerPersonalInfoUpdate passengerPersonalInfo)
        {
            try
            {
                Passenger passenger = Context.Passengers.First(pass => pass.Login == passengerPersonalInfo.OldLogin);

                if(passenger.Password == passengerPersonalInfo.OldPassword)
                {
                    passenger.Login = passengerPersonalInfo.Login;
                    passenger.Name = passengerPersonalInfo.Name;
                    passenger.Password = passengerPersonalInfo.Password;
                    passenger.StationId = null;

                    Context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Incorrect password!");
                }
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
                Owner owner = Context.Owners.First(own => own.Login == ownerPersonalInfo.OldLogin);

                if(owner.Password == ownerPersonalInfo.OldPassword)
                {
                    owner.Login = ownerPersonalInfo.Login;
                    owner.License = ownerPersonalInfo.License;
                    owner.Password = ownerPersonalInfo.Password;
                    owner.Name = ownerPersonalInfo.Name;

                    Context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Incorrect password!");
                }
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

                if(unit.Password == unitPersonalInfo.OldPassword)
                {
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
                else
                {
                    Console.WriteLine("Incorrect password!");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error in update unit info.");
            }
        }

        public void DeleteProfile(DeletePersonalInfoVerification delete)
        {
            delete.Category = delete.Category.ToString().ToUpper()[0];

            switch (delete.Category)
            {
                case 'P':
                    {
                        Passenger passenger;
                        try
                        {
                            passenger = Context.Passengers.First(pass => pass.Login == delete.Login) ;

                            if(passenger.Password == delete.Password)
                            {
                                Context.Passengers.Remove(passenger);
                                Context.SaveChanges();
                            }
                            else
                            {
                                Console.WriteLine("Password is not true.");
                            }
                            break;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Cant find passenger in DB.");
                            break;
                        }
                    }
                case 'O':
                    {
                        Owner owner;
                        try
                        {
                            owner = Context.Owners.First(own => own.Login == delete.Login);

                            if(owner.Password == delete.Password)
                            {
                                Context.Owners.Remove(owner);
                                Context.SaveChanges();
                            }
                            else
                            {
                                Console.WriteLine("Password is not true.");
                            }
                            break;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Cant find owner in DB.");
                            break;
                        }
                    }
                case 'D':
                    {
                        Unit unit;
                        try
                        {
                            unit = Context.Units.First(unt => unt.Login == delete.Login);

                            if(unit.Password == delete.Password)
                            {
                                Context.Units.Remove(unit);
                                Context.SaveChanges();
                            }
                            else
                            {
                                Console.WriteLine("Password is not true.");
                            }
                            break;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Cant find driver in DB.");
                            break;
                        }
                    }
                default:
                    {
                        Console.WriteLine("Bad category.");
                        break;
                    }
            }
        }
    }
}
