using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Entities
{
    public class EventTicketCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int Price { get; set; }
        public int Capacity {  get; set; }
    }
}
