using System;
using System.Collections.Generic;

namespace TTGServer.Models.DBModels
{
    public partial class Passenger
    {
        public string Name { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int? StationId { get; set; }
        public int Id { get; set; }

        public virtual Station? Station { get; set; }
    }
}
