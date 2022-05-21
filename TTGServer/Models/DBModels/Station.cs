using System;
using System.Collections.Generic;

namespace TTGServer.Models.DBModels
{
    public partial class Station
    {
        public Station()
        {
            Passengers = new HashSet<Passenger>();
        }

        public string? Description { get; set; }
        public int WayId { get; set; }
        public int Position { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Name { get; set; } = null!;
        public int Id { get; set; }

        public virtual Way Way { get; set; } = null!;
        public virtual ICollection<Passenger> Passengers { get; set; }
    }
}
