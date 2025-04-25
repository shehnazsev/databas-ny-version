using Data.Models;
using Data.Repositories;
using System.Collections.Generic;

namespace Data.Services
{
    public class KundService : IKundService
    {
        private readonly IKundRepository _kundRepository;

        public KundService(IKundRepository kundRepository)
        {
            _kundRepository = kundRepository;
        }

        public IEnumerable<Kund> GetAll() => _kundRepository.GetAll();

        public Kund GetById(int kundnummer) => _kundRepository.GetById(kundnummer);

        public void Add(Kund kund) => _kundRepository.Add(kund);

        public void Update(Kund kund) => _kundRepository.Update(kund);

        public void Delete(Kund kund) => _kundRepository.Delete(kund);
    }
}
