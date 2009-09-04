using NerdDinner.Entities;
using FluentNHibernate.Mapping;

namespace NerdDinner.Mappings
{
    public class DinnersMap : ClassMap<Dinners>
    {
        public DinnersMap()
        {
            Id(x => x.DinnerID).GeneratedBy.Identity();
            Map(x => x.Title);
            Map(x => x.EventDate);
            Map(x => x.Description);
            Map(x => x.HostedBy);
            Map(x => x.ContactPhone);
            Map(x => x.Address);
            Map(x => x.Country);
            Map(x => x.Latitude);
            Map(x => x.Longitude);

            HasMany(x => x.RSVPs)
                .WithTableName("RSVP")
                .KeyColumnNames.Add("DinnerID")
                .WithForeignKeyConstraintName("DinnerID")
                .Cascade.All()
                .Inverse();
                
        }
    }
}
