using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Entities
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
