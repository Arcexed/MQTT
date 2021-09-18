using System;
using System.Collections.Generic;

#nullable disable

namespace Models.Models
{
    public partial class Role
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Descr { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
