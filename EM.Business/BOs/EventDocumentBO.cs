using EM.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.BOs
{
    public class EventDocumentBO
    {
        
        public int Id { get; set; }
        public string Type { get; set; }
        public int EventId { get; set; }
        public  string Title { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
