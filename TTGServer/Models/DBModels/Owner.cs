using System;
using System.Collections.Generic;

namespace TTGServer.Models.DBModels
{
    public partial class Owner
    {
        public Owner()
        {
            Ways = new HashSet<Way>();
        }

        public string Name { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? License { get; set; }
        public int Id { get; set; }

        public virtual ICollection<Way> Ways { get; set; }
    }
}
