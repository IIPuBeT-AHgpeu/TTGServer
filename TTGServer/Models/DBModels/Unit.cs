using System;
using System.Collections.Generic;

namespace TTGServer.Models.DBModels
{
    public partial class Unit
    {
        public Unit()
        {
            WorkDays = new HashSet<WorkDay>();
        }

        public string? Model { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public bool IsFull { get; set; }
        public int WayId { get; set; }
        public string Number { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Passport { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Id { get; set; }

        public virtual Way Way { get; set; } = null!;
        public virtual ICollection<WorkDay> WorkDays { get; set; }
    }
}
