namespace TTGServer.Models.ServiceModels
{
    public class WorkDayModel
    {
        public int? Profit { get; set; }
        public DateOnly DateStart { get; set; }
        public DateOnly? DateEnd { get; set; }
        public int UnitId { get; set; }
    }
}
