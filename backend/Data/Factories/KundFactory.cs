using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Factories
{
    public class KundFactory
    {
        public Kund CreateKund(string namn, string telefonnummer)
        {
            return new Kund
            {
                Namn = namn,
                Telefonnummer = telefonnummer
            };
        }
    }
}
