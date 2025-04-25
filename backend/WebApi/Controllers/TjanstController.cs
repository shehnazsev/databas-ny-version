using Data.Factories;
using Data.Models;
using Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TjanstController : ControllerBase
    {
        private readonly ITjanstService _tjanstService;

        public TjanstController(ITjanstService tjanstService)
        {
            _tjanstService = tjanstService;
        }

        [HttpGet]
        public IEnumerable<Tjanst> Get() => _tjanstService.GetAll();
    }
}
