using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NerdDinner.Models;
using NerdDinner.Entities;

namespace NerdDinner.Tests.Fakes {

    public class FakeDinnerRepository : IDinnerRepository {

        private List<NerdDinner.Entities.Dinners> dinnerList;

        public FakeDinnerRepository(List<NerdDinner.Entities.Dinners> dinners) {
            dinnerList = dinners;
        }

        public IQueryable<NerdDinner.Entities.Dinners> FindAllDinners() {
            return dinnerList.AsQueryable();
        }

        public IQueryable<NerdDinner.Entities.Dinners> FindUpcomingDinners() {
            return (from dinner in dinnerList
                    where dinner.EventDate > DateTime.Now
                    select dinner).AsQueryable();
        }

        public IQueryable<NerdDinner.Entities.Dinners> FindByLocation(float lat, float lon) {
            return (from dinner in dinnerList
                    where dinner.Latitude == lat && dinner.Longitude == lon
                    select dinner).AsQueryable();
        }

        public NerdDinner.Entities.Dinners GetDinner(int id) {
            return dinnerList.SingleOrDefault(d => d.DinnerID == id);
        }

        public void Add(NerdDinner.Entities.Dinners dinner) {
            dinnerList.Add(dinner);
        }

        public void Delete(NerdDinner.Entities.Dinners dinner) {
            dinnerList.Remove(dinner);
        }

        public void Save(NerdDinner.Entities.Dinners dinner) {
            foreach (NerdDinner.Entities.Dinners d in dinnerList) {
                if (!d.IsValid)
                    throw new ApplicationException("Rule violations");
            }
        }
    }
}
