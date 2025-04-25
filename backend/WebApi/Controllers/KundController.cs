using Microsoft.AspNetCore.Mvc;
using Data.Factories;
using Data.Models;
using Data.Services;

[Route("api/[controller]")]
[ApiController]
public class KundController : ControllerBase
{
    private readonly IKundService _kundService;
    private readonly KundFactory _kundFactory;

    public KundController(IKundService kundService, KundFactory kundFactory)
    {
        _kundService = kundService;
        _kundFactory = kundFactory;
    }

    [HttpGet]
    public IEnumerable<Kund> Get() => _kundService.GetAll();

    [HttpGet("{kundnummer}")]
    public ActionResult<Kund> GetKund(int kundnummer)
    {
        var kund = _kundService.GetById(kundnummer);
        if (kund == null)
        {
            return NotFound();
        }
        return Ok(kund);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Kund kund)
    {
        var nyttKund = _kundFactory.CreateKund(kund.Namn, kund.Telefonnummer);

        _kundService.Add(nyttKund);

        return CreatedAtAction(nameof(Get), new { id = nyttKund.Kundnummer }, nyttKund);
    }

    [HttpPut("{kundnummer}")]
    public IActionResult Put(int kundnummer, [FromBody] Kund kund)
    {
        var existerandeKund = _kundService.GetById(kundnummer);
        if (existerandeKund == null)
        {
            return NotFound();
        }

        existerandeKund.Namn = kund.Namn;
        existerandeKund.Telefonnummer = kund.Telefonnummer;

        _kundService.Update(existerandeKund);
        return Ok(existerandeKund);
    }

    [HttpDelete("{kundnummer}")]
    public IActionResult Delete(int kundnummer)
    {
        var kund = _kundService.GetById(kundnummer);
        if (kund == null)
        {
            return NotFound();
        }

        _kundService.Delete(kund);
        return NoContent();
    }
}
