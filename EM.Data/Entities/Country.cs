using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Entities
{
    public class Country
    {
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// Name of the Country
        /// </summary>
        [Column("name")]
        public required string Name { get; set; }

        public ICollection<State> States { get; set; }
        public ICollection<TaxConfiguration> TaxConfigurations { get; set; }
    }

}