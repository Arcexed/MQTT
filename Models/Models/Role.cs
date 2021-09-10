using System;
using System.Collections.Generic;

#nullable disable

namespace Models.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Descr { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
