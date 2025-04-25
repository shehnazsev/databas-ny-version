using Data.Models;
using System.Collections.Generic;

namespace Data.Repositories
{
    public interface IKundRepository
    {
        IEnumerable<Kund> GetAll();
        Kund GetById(int kundnummer);
        void Add(Kund kund);
        void Update(Kund kund);
        void Delete(Kund kund);
    }


}
