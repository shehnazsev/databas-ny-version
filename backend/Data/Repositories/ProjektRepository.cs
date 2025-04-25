using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProjektRepository : IProjektRepository
    {
        private readonly AppDbContext _context;

        public ProjektRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Projekt> GetAll() => _context.Projekt
            .Include(p => p.Kund)
            .Include(p => p.Tjanst)
            .ToList();

        public Projekt GetById(int projektnummer) => _context.Projekt
            .Include(p => p.Kund)
            .Include(p => p.Tjanst)
            .FirstOrDefault(p => p.Projektnummer == projektnummer);

        public void Add(Projekt projekt)
        {
            if(projekt.Kundnummer != 0 )
            {
                var kund = _context.Kunder.FirstOrDefault(k => k.Kundnummer == projekt.Kundnummer);

                if(kund != null)
                {
                    projekt.Kund = kund;
                }
            }

            _context.Projekt.Add(projekt);
            _context.SaveChanges();
        }

        public void Update(Projekt projekt)
        {
            var existerandeProjekt = _context.Projekt
                .Include(p => p.Kund)
                .FirstOrDefault(p => p.Projektnummer == projekt.Projektnummer);

            if (existerandeProjekt == null) return;

            var tjanst = _context.Tjanster.FirstOrDefault(t => t.TjanstId == projekt.TjanstId);

            if (tjanst != null)
            {
                existerandeProjekt.Totalpris = projekt.AntalTimmar * tjanst.PrisPerTimme;
            }


            existerandeProjekt.Namn = projekt.Namn;
            existerandeProjekt.Startdatum = projekt.Startdatum;
            existerandeProjekt.Slutdatum = projekt.Slutdatum;
            existerandeProjekt.Projektansvarig = projekt.Projektansvarig;
            existerandeProjekt.Kundnummer = projekt.Kundnummer;
            existerandeProjekt.TjanstId = projekt.TjanstId;
            existerandeProjekt.Status = projekt.Status;
            existerandeProjekt.AntalTimmar = projekt.AntalTimmar;

            var kund = _context.Kunder.FirstOrDefault(k => k.Kundnummer == projekt.Kundnummer);
            if(kund != null)
            {
                existerandeProjekt.Kund = kund;
            }

            _context.SaveChanges();
        }
    }
}
