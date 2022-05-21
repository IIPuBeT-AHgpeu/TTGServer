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
            try
            {
                Unit unit = Context.Units.First(unt => unt.Login == login);

                if(unit.Status != status && (status == "Эксплуатируется" || status == "На ремонте"))
                {
                    unit.Status = status;

                    Context.SaveChanges();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error in change unit status.");
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

        public void UpdatePassengerPosition(PassengerWaiting passenger)
        {
            if(passenger.IsSet)
            {
                try
                {
                    //Get wayId
                    int wayId = Context.Ways.First(way => way.Name == passenger.WayName).Id;
                    //Get stationId
                    int stationId = Context.Stations.First(station => 
                        (
                            station.Latitude == passenger.StationLatitude && 
                            station.Longitude == passenger.StationLongitude) &&
                            station.WayId == wayId
                        ).Id;

                    Passenger _passenger = Context.Passengers.First(pass => pass.Login == passenger.Login);
                    _passenger.StationId = stationId;

                    Context.SaveChanges();
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in update passenger.");
                }
            }
            else
            {
                try
                {
                    Passenger _passenger = Context.Passengers.First(pass => pass.Login == passenger.Login);
                    _passenger.StationId = null;

                    Context.SaveChanges();
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in update passenger.");
                }
            }
        }

        public void AddNewWay(WayModel newWay)
        {
            try
            {
                //Get ownerId
                int ownerId = Context.Owners.First(owner => owner.Login == newWay.OwnerLogin).Id;

                Context.Ways.Add(new Way()
                {
                    Name = newWay.Name,
                    OwnerId = ownerId,
                    Price = newWay.Price,
                    Rent = newWay.Rent
                });
                Context.SaveChanges();
            }
            catch (Exception)
            {
                Console.WriteLine("Error in add way.");
            }
        }

        public void UpdateWay(string oldName, WayModel newWayInfo)
        {
            try
            {
                //Get way
                Way way = Context.Ways.First(_way => _way.Name == oldName);
                string trueLogin = Context.Owners.First(owner => owner.Id == way.OwnerId).Login;
                //Проверка на подлинность
                if(newWayInfo.OwnerLogin == trueLogin)
                {
                    //Update way
                    way.Price = newWayInfo.Price;
                    way.Name = newWayInfo.Name;
                    way.Rent = newWayInfo.Rent;
                    //Save changes
                    Context.SaveChanges();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error in update way.");
            }
        }

        public void DeleteWay(string wayName, string ownerLogin)
        {
            try
            {
                //Get way
                Way way = Context.Ways.First(_way => _way.Name == wayName);
                string trueLogin = Context.Owners.First(owner => owner.Id == way.OwnerId).Login;

                if(trueLogin == ownerLogin)
                {
                    Context.Ways.Remove(way);
                    Context.SaveChanges();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error in delete way.");
            }
        }

        public void SetStartWorkDay(StartWorkDayModel start)
        {
            try
            {
                //Get unitId
                int unitId = Context.Units.First(unit => unit.Login == start.Login).Id;

                Context.WorkDays.Add(new WorkDay()
                {
                    UnitId = unitId,
                    DateStart = DateOnly.Parse(start.DateStart),
                    DateEnd = null,
                    Profit = null
                });
                Context.SaveChanges();
            }
            catch (Exception)
            {
                Console.WriteLine("Error in create workday.");
            }
        }

        public void SetFinishWorkDay(EndWorkDayModel end)
        {
            try
            {
                //Get unitId
                int unitId = Context.Units.First(unit => unit.Login == end.Login).Id;

                //Get active workDay
                WorkDay currentWorkDay = Context.WorkDays.First(workday => 
                    (workday.UnitId == unitId && workday.DateEnd == null && workday.Profit == null)
                );

                currentWorkDay.DateEnd = DateOnly.Parse(end.DateEnd);
                currentWorkDay.Profit = end.Profit;

                Context.SaveChanges();
            }
            catch (Exception)
            {
                Console.WriteLine("Error in update workday.");
            }
        }

        public bool IsWorkDayExists(string unitLogin)
        {
            try
            {
                //Get unitId
                int unitId = Context.Units.First(unit => unit.Login == unitLogin).Id;

                //Get active workDay
                WorkDay currentWorkDay = Context.WorkDays.First(workday =>
                    (workday.UnitId == unitId && workday.DateEnd == null && workday.Profit == null)
                );

                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Error in IsWorkDayExists.");
                return false;
            }
        }

        public void SetStartTrip(StartTripModel trip)
        {
            try
            {
                //Get unitId
                int unitId = Context.Units.First(unit => unit.Login == trip.Login).Id;

                WorkDay currentWorkDay = Context.WorkDays.First(workday =>
                    (workday.UnitId == unitId && workday.DateEnd == null && workday.Profit == null)
                );

                Context.Trips.Add(new Trip()
                {
                    TimeStart = TimeOnly.Parse(trip.TimeStart),
                    WorkdayId = currentWorkDay.Id,
                    Date = DateOnly.Parse(trip.Date),
                    TimeEnd = null
                });
                Context.SaveChanges();
            }
            catch (Exception)
            {
                Console.WriteLine("Error in SetStartTrip.");
            }
        }

        public void SetFinishTrip(EndTripModel trip)
        {
            try
            {
                //Get unitId
                int unitId = Context.Units.First(unit => unit.Login == trip.Login).Id;

                WorkDay currentWorkDay = Context.WorkDays.First(workday =>
                    (workday.UnitId == unitId && workday.DateEnd == null && workday.Profit == null)
                );

                Trip _trip = Context.Trips.First(trp => trp.WorkdayId == currentWorkDay.Id && trp.TimeEnd == null);

                _trip.TimeEnd = TimeOnly.Parse(trip.TimeEnd);
                Context.SaveChanges();
            }
            catch (Exception)
            {
                Console.WriteLine("Error in SetSFinishTrip.");
            }
        }

        public bool IsStartTripExists(string login)
        {
            try
            {
                //Get unitId
                int unitId = Context.Units.First(unit => unit.Login == login).Id;

                WorkDay currentWorkDay = Context.WorkDays.First(workday =>
                    (workday.UnitId == unitId && workday.DateEnd == null && workday.Profit == null)
                );

                Trip _trip = Context.Trips.First(trp => trp.WorkdayId == currentWorkDay.Id && trp.TimeEnd == null);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
