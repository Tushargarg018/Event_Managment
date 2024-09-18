using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.BOs
{
    public class PagedEventBO
    {
        public List<EventBO> Events { get; set; }
        public int TotalRecords { get; set; }

        public PagedEventBO(List<EventBO> events, int totalRecords)
        {
            Events = events;
            TotalRecords = totalRecords;
        }
    }
}
