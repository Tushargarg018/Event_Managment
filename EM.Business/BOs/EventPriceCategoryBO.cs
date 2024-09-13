using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EM.Business.BOs
{
    public class EventPriceCategoryBO
    {
        
        public int Id { get; set; }

        
        public string Name { get; set; }

       
        public int EventId { get; set; }

        
        public int Price { get; set; }

        
        public int Capacity { get; set; }

      
        public DateTime CreatedOn { get; set; }

       
        public DateTime UpdatedOn { get; set; }

    }
}
