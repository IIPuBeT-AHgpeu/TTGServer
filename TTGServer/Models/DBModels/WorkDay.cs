using System;
using System.Collections.Generic;

namespace TTGServer.Models.DBModels
{
    public partial class WorkDay
    {
        public WorkDay()
        {
            Trips = new HashSet<Trip>();
        }

        public int? Profit { get; set; }
        public DateOnly DateStart { get; set; }
        public DateOnly? DateEnd { get; set; }
        public int UnitId { get; set; }
        public int Id { get; set; }

        public virtual Unit Unit { get; set; } = null!;
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
