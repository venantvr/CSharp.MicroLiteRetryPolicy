using System;
using MicroLite.Mapping;
using MicroLite.Mapping.Attributes;

namespace Business.Data
{
    [Table("Customers")]
    public class Customers
    {
        [Column("ContactID")]
        [Identifier(IdentifierStrategy.DbGenerated)] // Uniquement pour l'update du Test #3
        public virtual int ContactId { get; set; } // Case par rapport à la BD

        [Column("CustomerTypeID")] // Exception si aucun TableAttribute
        public int CustomerTypeId { get; set; }

        public DateTime? InitialDate { get; set; }
        public int? PrimaryDesintation { get; set; }
        public int? SecondaryDestination { get; set; }
        public int? PrimaryActivity { get; set; }
        public int? SecondaryActivity { get; set; }

        [Column("Notes")] // Uniquement pour l'update du Test #3
        public string Notes { get; set; }

        [Column("Notes")] // Uniquement pour l'update du Test #3
        public byte[] RowVersion { get; set; } // convert timestamp sql to C# ?
    }
}