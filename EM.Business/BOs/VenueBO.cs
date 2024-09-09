using EM.Core.Enums;
using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.BOs
{
    public class VenueBO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public VenueTypeEnum Type { get; set; }
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
         
    }
}
