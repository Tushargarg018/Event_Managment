using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EM.Data.Entities
{
    public class TaxConfiguration
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("country_id")]
        public int CountryId { get; set; }

        [Column("state_id")]
        public int StateId { get; set; }

        [Column("tax_details")]
        public required JsonDocument TaxDetails { get; set; }


        public Country Country { get; set; }
        public State State { get; set; }

    }
}