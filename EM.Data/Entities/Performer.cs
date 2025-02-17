﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace EM.Data.Entities
{
    public class Performer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// name of the performer
        /// </summary>
        [Column("name")]
		public required string Name { get; set; }

        /// <summary>
        /// Bio of the performer
        /// </summary>
        [Column("bio")]
		public required string Bio {  get; set; }

        /// <summary>
        /// Profile PIC of the performer
        /// </summary>
        /// 
        [Column("profile")]
		public string? Profile { get; set; }

        /// <summary>
        /// creation date of the performer
        /// </summary>
        /// 
        [Column("created_on")]
		public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Modified date of the performer
        /// </summary>
        [Column("modified_on")]
		public DateTime ModifiedOn { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
