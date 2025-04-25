using Data.Models;

namespace Data.Repositories
{
    public interface ITjanstRepository
    {
        IEnumerable<Tjanst> GetAll();
    }

    public class TjanstRepository : ITjanstRepository {
        private readonly AppDbContext _context;

        public TjanstRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Tjanst> GetAll() => _context.Tjanster.ToList();
    }
}
