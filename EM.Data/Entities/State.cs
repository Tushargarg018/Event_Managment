using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Entities
{
    public class State
    {
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Name of the state
        /// </summary>
        [Column("name")]
		public required string Name { get; set; }

        /// <summary>
        /// country id
        /// </summary>
        [Column("country_id")]
		public int? CountryId { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
