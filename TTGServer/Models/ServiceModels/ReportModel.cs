namespace TTGServer.Models.ServiceModels
{
    public class ReportModel
    {
        public List<DynamicAvr> AvrTimes { get; set; }
        public List<DynamicProfit> AvrProfits { get; set; }
        public List<DriverProfit> DriversWorkInfo { get; set; }
        public List<TripReportModel> BestWorkDays { get; set; }
        public List<TripReportModel> WorstWorkDays { get; set; }

        public ReportModel()
        {
            AvrProfits = new List<DynamicProfit>();
            AvrTimes = new List<DynamicAvr>();
            DriversWorkInfo = new List<DriverProfit>();
            BestWorkDays = new List<TripReportModel>();
            WorstWorkDays = new List<TripReportModel>();
        }
    }
}
