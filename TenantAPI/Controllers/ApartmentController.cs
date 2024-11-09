using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TenantAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class ApartmentController : ControllerBase
{
    private static List<Apartment> _apartments = new List<Apartment>
    {
        new Apartment { Id = 1, ApartmentNumber = 200, OwnerName = "Maria Sanchez", PhoneNumber = "8295284076" }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Apartment>> GetApartments()
    {
        return Ok(_apartments);
    }

    [HttpGet("{id}")]
    public ActionResult<Apartment> GetApartment(int id)
    {
        var apartment = _apartments.FirstOrDefault(a => a.Id == id);
        if (apartment == null)
        {
            return NotFound(new { message = "Apartment not found" });
        }
        return Ok(apartment);
    }

    [HttpPost]
    public ActionResult<Apartment> CreateApartment([FromBody] Apartment apartment)
    {
        if (string.IsNullOrWhiteSpace(apartment.OwnerName) ||
            string.IsNullOrWhiteSpace(apartment.PhoneNumber) ||
            apartment.ApartmentNumber <= 0)
        {
            return BadRequest(new { message = "Invalid data" });
        }
        apartment.Id = _apartments.Count > 0 ? _apartments.Max(a => a.Id) + 1 : 1;
        _apartments.Add(apartment);
        return CreatedAtAction(nameof(GetApartment), new { id = apartment.Id }, apartment);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateApartment(int id, [FromBody] Apartment updatedApartment)
    {
        var apartment = _apartments.FirstOrDefault(a => a.Id == id);
        if (apartment == null)
        {
            return NotFound(new { message = "Apartment not found" });
        }

        apartment.ApartmentNumber = updatedApartment.ApartmentNumber;
        apartment.OwnerName = updatedApartment.OwnerName;
        apartment.PhoneNumber = updatedApartment.PhoneNumber;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteApartment(int id)
    {
        var apartment = _apartments.FirstOrDefault(a => a.Id == id);
        if (apartment == null)
        {
            return NotFound(new { message = "Apartment not found" });
        }

        _apartments.Remove(apartment);
        return NoContent();
    }
}
