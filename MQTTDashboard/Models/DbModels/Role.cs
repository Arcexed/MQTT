using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQTTDashboard.Models.DbModels
{
    public class Role
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Descr { get; set; }

        public Role()
        {
            Users = new HashSet<User>();
        }

        public virtual ICollection<User> Users { get; set; }
    }
}
