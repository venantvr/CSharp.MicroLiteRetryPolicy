using System;
using MicroLite.Mapping.Attributes;

namespace Business.Business
{
    [Table("Customers")]
    public class ReadOnlyCustomers_V2 // : Customers // En attendant la version 7.0 qui gère les types immutables
    {
        public ReadOnlyCustomers_V2() // Test #3
        {
        }

        //public ulong /*int*/ RowVersion { get; set; } // convert timestamp sql to C# ?

        //public ReadOnlyCustomers_V2(int contactId, int customerTypeId, DateTime? initialDate, int? primaryDesintation, int? secondaryDestination, int? primaryActivity, int? secondaryActivity, string notes)
        //{
        //    _contactId = contactId;
        //    _customerTypeId = customerTypeId;
        //    _initialDate = initialDate;
        //    _primaryDesintation = primaryDesintation;
        //    _secondaryDestination = secondaryDestination;
        //    _primaryActivity = primaryActivity;
        //    _secondaryActivity = secondaryActivity;
        //    _notes = notes;
        //}

        public ReadOnlyCustomers_V2(dynamic expando)
        {
            ContactId = expando.ContactID;
            CustomerTypeId = expando.CustomerTypeID;
            InitialDate = expando.InitialDate;
            PrimaryDesintation = expando.PrimaryDesintation;
            SecondaryDestination = expando.SecondaryDestination;
            PrimaryActivity = expando.PrimaryActivity;
            SecondaryActivity = expando.SecondaryActivity;
            Notes = expando.Notes;
            RowVersion = expando.RowVersion;
        }

        [Column("ContactID", false, false)]
        public int ContactId { get; } // Case par rapport à la BD

        [Column("CustomerTypeID", false, false)] // Exception si aucun TableAttribute
        public int CustomerTypeId { get; } // Case par rapport à la BD

        public DateTime? InitialDate { get; }

        public int? PrimaryDesintation { get; }

        public int? SecondaryDestination { get; }

        public int? PrimaryActivity { get; }

        public int? SecondaryActivity { get; }

        public string Notes { get; private set; }

        public byte[] RowVersion { get; }

        //public Customers GetCustomers()
        //{
        //    return new Customers()
        //           {
        //               ContactId = ContactId,
        //               CustomerTypeId = CustomerTypeId,
        //               InitialDate = InitialDate,
        //               PrimaryDesintation = PrimaryDesintation,
        //               SecondaryDestination = SecondaryDestination,
        //               PrimaryActivity = PrimaryActivity,
        //               SecondaryActivity = SecondaryActivity,
        //               Notes = Notes
        //           };
        //}

        public void ModifyNotes(string notes)
        {
            Notes = notes;
        }
    }
}