using System;
using MicroLite.Mapping.Attributes;

namespace Business.Business
{
    [Table("Customers")]
    public class ReadOnlyCustomers_V1 // En attendant la version 7.0 qui gère les types immutables
    {
        //public ulong /*int*/ RowVersion { get; set; } // convert timestamp sql to C# ?

        public ReadOnlyCustomers_V1(int contactId, int customerTypeId, DateTime? initialDate, int? primaryDesintation, int? secondaryDestination, int? primaryActivity, int? secondaryActivity, string notes)
        {
            ContactId = contactId;
            CustomerTypeId = customerTypeId;
            InitialDate = initialDate;
            PrimaryDesintation = primaryDesintation;
            SecondaryDestination = secondaryDestination;
            PrimaryActivity = primaryActivity;
            SecondaryActivity = secondaryActivity;
            Notes = notes;
        }

        //[Column("ContactID", false, false)]
        public int ContactId { get; } // Case par rapport à la BD

        //[Column("CustomerTypeID", false, false)] // Exception si aucun TableAttribute
        public int CustomerTypeId { get; }

        // Case par rapport à la BD

        //[Column("InitialDate", false, false)]
        public DateTime? InitialDate { get; }

        //[Column("PrimaryDesintation", false, false)]
        public int? PrimaryDesintation { get; }

        //[Column("SecondaryDestination", false, false)]
        public int? SecondaryDestination { get; }

        //[Column("PrimaryActivity", false, false)]
        public int? PrimaryActivity { get; }

        //[Column("SecondaryActivity", false, false)]
        public int? SecondaryActivity { get; }

        //[Column("Notes", false, false)]
        public string Notes { get; }
    }
}