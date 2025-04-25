using Data.Models;
using System.Collections.Generic;

namespace Data.Services
{
    public interface IKundService
    {
        IEnumerable<Kund> GetAll();
        Kund GetById(int kundnummer);
        void Add(Kund kund);
        void Update(Kund kund);
        void Delete(Kund kund);
    }
}
