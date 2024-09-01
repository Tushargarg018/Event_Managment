using EM.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Entities
{

    public class Venue
    {
        public int Id  { get; set; }
        public string Name { get; set; }
        public VenueTypeEnum Type {  get; set; }
        public int MaxCapacity { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int ZipCode { get; set; }
        public int City { get; set; }
        public int State { get; set; }
        public int Country { get; set; }
        public DateTime Created_on { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int OrganizerId { get; set; }
        public string Description { get; set; }
        public Organizer Organizer { get; set; }
        
        public virtual ICollection<Event> Events { get; set; }
    }
}
