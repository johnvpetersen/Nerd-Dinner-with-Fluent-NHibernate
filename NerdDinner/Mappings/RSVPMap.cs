using NerdDinner.Entities;
using FluentNHibernate.Mapping;

namespace NerdDinner.Mappings
{
    public class RSVPMap : ClassMap<RSVP>
    {
        public RSVPMap()
        {
            Id(x => x.RsvpID).GeneratedBy.Identity();
            Map(x => x.AttendeeName);
            References(x => x.dinner)
                .WithColumns(x => x.DinnerID);
        }
    }
}
