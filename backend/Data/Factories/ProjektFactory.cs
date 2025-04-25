using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Factories
{
    public class ProjektFactory
    {
        private readonly AppDbContext _context;

        public ProjektFactory(AppDbContext context)
        {
            _context = context;
        }

        public Projekt CreateProjekt(string namn, DateTime startdatum, DateTime? slutdatum,
            string projektansvarig, int kundnummer, int tjanstId, int antalTimmar, ProjektStatus status)
        {
            // Hämta kund och tjänst
            var kund = _context.Kunder.FirstOrDefault(k => k.Kundnummer == kundnummer);
            if (kund == null) throw new Exception("kund kunde inte hittas");

            var tjanst = _context.Tjanster.FirstOrDefault(t => t.TjanstId == tjanstId);
            if (tjanst == null) throw new Exception("tjänst kunde inte hittas");
            
            var totalpris = tjanst.PrisPerTimme * antalTimmar;

            // Skapa projekt
            var projekt = new Projekt
            {
                Namn = namn,
                Startdatum = startdatum,
                Slutdatum = slutdatum,
                Projektansvarig = projektansvarig,
                Kund = kund,
                Tjanst = tjanst,
                Totalpris = totalpris,
                Status = status,
                TjanstId = tjanstId,
                AntalTimmar = antalTimmar
            };

            return projekt;
        }
    }
}
