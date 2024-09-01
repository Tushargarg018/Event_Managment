using EM.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Entities
{

    public class EventDocument
    {
        public int Id { get; set; }
        public DocumentType Type { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
