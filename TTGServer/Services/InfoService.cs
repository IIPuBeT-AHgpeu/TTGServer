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
                var tripsTime = from trip in Context.Trips
                                join workday in Context.WorkDays on trip.WorkdayId equals workday.Id
                                join unit in Context.Units on workday.UnitId equals unit.Id
                                join _way in Context.Ways on unit.WayId equals _way.Id
                                where _way.Name == wayName
                                select new { StartTime = trip.TimeStart, EndTime = trip.TimeEnd };

                float? sum = 0;

                foreach (var trip in tripsTime)
                {
                    sum += (trip.EndTime?.Hour * 60 + trip.EndTime?.Minute + (float?)trip.EndTime?.Second / 60) 
                        - ((float)trip.StartTime.Hour * 60 + trip.StartTime.Minute + (float)trip.StartTime.Second / 60);
                }

                wayInformation.AvrTripTime = sum/tripsTime.Count();//

                MapInfoService mapInfoService = new MapInfoService(Context);

                //Get Stations
                wayInformation.Stations = mapInfoService.GetStationsMapInfo(wayName);
                //Get cars
                wayInformation.Cars = mapInfoService.GetUnitsMapInfo(wayName);

                return wayInformation;
            }
            catch (Exception)
            {
                return null;
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

        public void UpdateStationsList(UpdateStationListModel model)
        {
            try
            {
                //Get way
                Way way = Context.Ways.First(_way => _way.Name == model.WayName);
                //Get trueLogin
                string trueLogin = Context.Owners.First(owner => owner.Id == way.OwnerId).Login;

                if(trueLogin == model.OwnerLogin)
                {
                    List<Station> stations = Context.Stations.Where(station => station.WayId == way.Id).ToList();

                    foreach (Station station in stations)
                    {
                        Context.Stations.Remove(station);
                    }

                    foreach (StationModel station in model.Stations)
                    {
                        Context.Stations.Add(new Station()
                        {
                            Name = station.Name,
                            Description = station.Description,
                            Latitude = station.Latitude,
                            Longitude = station.Longitude,
                            WayId = way.Id
                        });
                    }
                    Context.SaveChanges();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {

                
            }
        }

        public ReportModel GetReport(InputDataForReportModel data)
        {
            ReportModel report = new ReportModel();
            try
            {
                //Get wayId by WayName
                Way way = Context.Ways.First(way => way.Name == data.WayName);
                //Get avrTime
                var values = from trip in Context.Trips
                                join workday in Context.WorkDays on trip.WorkdayId equals workday.Id
                                join unit in Context.Units on workday.UnitId equals unit.Id
                                join _way in Context.Ways on unit.WayId equals _way.Id
                                where _way.Id == way.Id
                                    && trip.Date < DateOnly.Parse(data.EndDate)
                                    && trip.Date >= DateOnly.Parse(data.StartDate)
                                select new { 
                                    StartTime = trip.TimeStart, 
                                    EndTime = trip.TimeEnd, 
                                    TripDate = trip.Date, 
                                };
                float? sumTime;
                int countTime;

                for(DateOnly d = DateOnly.Parse(data.StartDate); d < DateOnly.Parse(data.EndDate); d.AddMonths(1))
                {
                    sumTime = 0;
                    countTime = 0;
                    foreach (var v in values)
                    {
                        if(v.TripDate.Month == d.Month && v.TripDate.Year == d.Year)
                        {
                            sumTime += (v.EndTime?.Hour * 60 + v.EndTime?.Minute + (float?)v.EndTime?.Second / 60)
                                - ((float)v.StartTime.Hour * 60 + v.StartTime.Minute + (float)v.StartTime.Second / 60);
                            countTime++;
                        }
                    }
                    if(countTime > 0)
                    {
                        report.AvrTimes.Add(new DynamicAvr() { AvrTime = (float)(sumTime / countTime), Date = d });
                    }
                    else
                    {
                        report.AvrTimes.Add(new DynamicAvr() { AvrTime = 0, Date = d });
                    }
                }
                ////undone

                return report;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string[] GetOwnersWayNames(string login)
        {
            try
            {
                //Get ownerId
                int ownerId = Context.Owners.First(owner => owner.Login == login).Id;

                List<Way> ways = Context.Ways.Where(way => way.OwnerId == ownerId).ToList();

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
    }
}
