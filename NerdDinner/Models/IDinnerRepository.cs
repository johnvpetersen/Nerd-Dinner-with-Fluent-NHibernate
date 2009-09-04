using System.Linq;

namespace NerdDinner.Models {

    public interface IDinnerRepository {

        IQueryable<NerdDinner.Entities.Dinners> FindAllDinners();
        IQueryable<NerdDinner.Entities.Dinners> FindByLocation(float latitude, float longitude);
        IQueryable<NerdDinner.Entities.Dinners> FindUpcomingDinners();
        NerdDinner.Entities.Dinners GetDinner(int id);

        void Add(NerdDinner.Entities.Dinners dinner);
        void Delete(NerdDinner.Entities.Dinners dinner);

        void Save(NerdDinner.Entities.Dinners dinner);
    }
}
