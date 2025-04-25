using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class KundRepository : IKundRepository
    {
        private readonly AppDbContext _context;

        public KundRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Kund> GetAll() => _context.Kunder.ToList();

        public Kund GetById(int kundnummer) => _context.Kunder.Find(kundnummer);

        public void Add(Kund kund)
        {
            _context.Kunder.Add(kund);
            _context.SaveChanges();
        }

        public void Update(Kund kund)
        {
            _context.Kunder.Update(kund);
            _context.SaveChanges();
        }

        public void Delete(Kund kund)
        {
            _context.Kunder.Remove(kund);
            _context.SaveChanges();
        }
    }
}
