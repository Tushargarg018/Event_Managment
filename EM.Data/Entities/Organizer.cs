using EM.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Entities
{
    public class Organizer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserStatus Status {get; set;}
        public DateTime  CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual ICollection<Performer> Performers { get; set; }
        public virtual ICollection<Venue> Venues { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
