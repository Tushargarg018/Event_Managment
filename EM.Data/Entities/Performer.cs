using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Entities
{
    public class Performer
    {
        public int Id { get; set; }
        public string Name { get; set; }   
        public string Bio {  get; set; }
        public string Profile { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int OrganizerId { get; set; }
        public Organizer Organizer { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
