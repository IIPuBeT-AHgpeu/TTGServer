using System;
using System.Collections.Generic;

namespace TTGServer.Models.DBModels
{
    public partial class Way
    {
        public Way()
        {
            Stations = new HashSet<Station>();
            Units = new HashSet<Unit>();
        }

        public int OwnerId { get; set; }
        public float Price { get; set; }
        public string Name { get; set; } = null!;
        public float Rent { get; set; }
        public int Id { get; set; }

        public virtual Owner Owner { get; set; } = null!;
        public virtual ICollection<Station> Stations { get; set; }
        public virtual ICollection<Unit> Units { get; set; }
    }
}
