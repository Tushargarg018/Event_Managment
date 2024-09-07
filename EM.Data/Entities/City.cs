using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Entities
{
    public class City
    {
       
        [Column("id")]
        public int Id { get; set; }

		/// <summary>
		/// Name of the city
		/// </summary>
		[Column("name")]
        public required string Name { get; set; }

		/// <summary>
		/// State Id
		/// </summary>
		[Column("state_id")]
        public int StateId { get; set; }

        public State State { get; set; }    
    }
}
 