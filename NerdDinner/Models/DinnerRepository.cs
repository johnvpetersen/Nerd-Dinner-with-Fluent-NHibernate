using System;
using System.Linq;

using NerdDinner.Entities;
using NHibernate;
using NHibernate.Criterion;

namespace NerdDinner.Models
{
    public class DinnerRepository : NerdDinner.Models.IDinnerRepository
    {
        private ISession _session;


        public DinnerRepository(ISession session)
        {
            _session = session;

        }

        // Query Methods

        public IQueryable<NerdDinner.Entities.Dinners> FindAllDinners()
        {
            return _session.CreateCriteria(typeof (NerdDinner.Entities.Dinners))
                .List<NerdDinner.Entities.Dinners>().AsQueryable();
        }


        public IQueryable<NerdDinner.Entities.Dinners> FindUpcomingDinners()
        {
            return _session.CreateCriteria(typeof (NerdDinner.Entities.Dinners))
                .AddOrder(Order.Desc("EventDate"))
                .Add(Expression.Gt("EventDate", DateTime.Now))
                .List<NerdDinner.Entities.Dinners>().AsQueryable();
        }


        public IQueryable<NerdDinner.Entities.Dinners> FindByLocation(float latitude, float longitude)
        {
            IQueryable<NerdDinner.Entities.Dinners> dinners;
            dinners =
                _session.CreateSQLQuery(
                    "Select d.* from Dinners d join dbo.NearestDinners(:lat,:long) nd on d.Dinnerid = nd.Dinnerid where EventDate > :ed order by EventDate Desc")
                    .AddEntity(typeof (NerdDinner.Entities.Dinners))
                    .SetDateTime("ed", DateTime.Now)
                    .SetDouble("lat", latitude)
                    .SetDouble("long", longitude)
                    .List<NerdDinner.Entities.Dinners>().AsQueryable();

            return dinners;
        }

        public NerdDinner.Entities.Dinners GetDinner(int id)
        {
            var dinner = _session.Get<NerdDinner.Entities.Dinners>(id);
            _session.Flush();

            return dinner;
        }

        //
        // Insert/Delete Methods

        public void Add(Dinners dinner)
        {
            _session.SaveOrUpdate(dinner);
            _session.Flush();
        }

        public void Delete(NerdDinner.Entities.Dinners dinner)
        {
            _session.Delete(dinner);
            _session.Flush();
        }

        //
        // Persistence 

        public void Save(NerdDinner.Entities.Dinners dinner)
        {
            _session.SaveOrUpdate(dinner);
            _session.Flush();
        }
    }
}