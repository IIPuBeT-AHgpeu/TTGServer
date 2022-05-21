using TTGServer.Models.DBModels;
using TTGServer.Models.ServiceModels;

namespace TTGServer.Services
{
    public class InfoService : IService
    {
        public TTG_ver3Context? Context { get; set; }

        public InfoService(TTG_ver3Context? context)
        {
            Context = context;
        }

        public IProfileInfo? GetProfileInfo(char category, string login)
        {
            category = category.ToString().ToUpper()[0];

            switch(category)
            {
                case 'P':
                    {
                        Passenger passenger;
                        try
                        {
                            passenger = Context.Passengers.First(pass => pass.Login == login);

                            return (PassengerProfileInfo)passenger;
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

                            return (OwnerProfileInfo)owner;
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

                            return (DriverProfileInfo)unit;
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

        public string[] GetAllWayNames()
        {
            try
            {
                List<Way> ways = Context.Ways.ToList();

                string[] names = new string[ways.Count];

                for (int i = 0; i < ways.Count; i++)
                {
                    names[i] = ways[i].Name;
                }

                return names;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void ChangeUnitStatus(string login, string status)
        {
            //Протестить
            try
            {
                Unit unit = Context.Units.First(unt => unt.Login == login);

                if(unit.Status != status && (status == "Эксплуатируется" || status == "На ремонте"))
                {
                    unit.Status = status;

                    Context.SaveChanges();
                }
            }
            catch (Exception)
            {
            }
        }

        public WayInformation? GetWayInformation(string wayName)
        {
            try
            {
                //Get wayId by WayName
                Way way = Context.Ways.First(way => way.Name == wayName);
                WayInformation wayInformation = new WayInformation() { Price = way.Price };

                //Get avrTime
                wayInformation.AvrTripTime = 0;//

                //Get stations


                //Get cars


                return wayInformation;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
