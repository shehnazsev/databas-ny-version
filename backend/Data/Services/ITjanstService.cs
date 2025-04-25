using Data.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public interface ITjanstService
    {
        IEnumerable<Tjanst> GetAll();
    }

    public class TjanstService : ITjanstService
    {
        private readonly ITjanstRepository _tjanstRepository;

        public TjanstService(ITjanstRepository tjanstRepository)
        {
            _tjanstRepository = tjanstRepository;
        }

        public IEnumerable<Tjanst> GetAll() => _tjanstRepository.GetAll();
    }
}
