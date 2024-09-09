using EM.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Entities
{

    public class EventDocument
    {
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Document Type
        /// </summary>
        [Column("type")]
        public DocumentType Type { get; set; }

        /// <summary>
        /// event id
        /// </summary>
        [Column("event_id")]
        public int EventId { get; set; }

        /// <summary>
        /// title of the document
        /// </summary>
        [Column("title")]
		public required string Title { get; set; }

        /// <summary>
        /// path of the document
        /// </summary>
        [Column("file_path")]
		public required string FilePath { get; set; }

        /// <summary>
        /// file type of the document
        /// </summary>
        [Column("file_type")]
		public required string FileType { get; set; }

        /// <summary>
        /// Creation date of the document
        /// </summary>
        [Column("created_on")]
		public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Modified date of the document
        /// </summary>
        [Column("modified_on")]
		public DateTime ModifiedOn { get; set; }

		public Event Event { get; set; }
	}
}
