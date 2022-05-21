using System;
using System.Collections.Generic;

namespace TTGServer.Models.DBModels
{
    public partial class Trip
    {
        public DateOnly Date { get; set; }
        public TimeOnly TimeStart { get; set; }
        public TimeOnly? TimeEnd { get; set; }
        public int WorkdayId { get; set; }
        public int Id { get; set; }

        public virtual WorkDay Workday { get; set; } = null!;
    }
}
