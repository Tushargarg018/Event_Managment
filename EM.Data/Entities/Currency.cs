using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Entities
{
    public class Currency
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("country_id")]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        [Column("currency_code")]
        public string CurrencyCode { get; set; }

        [Column("symbol")]
        public string? Symbol { get; set; }
    }
}
