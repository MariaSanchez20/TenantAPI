using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TenantAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class ElectricityConsumptionController : ControllerBase
{
    private static List<ElectricityConsumption> _consumptions = new List<ElectricityConsumption>
    {
        new ElectricityConsumption { Id = 1, ApartmentId = 1, Date = new DateTime(2024, 7, 30), QuantityKw = 500 }
    };

    [HttpGet]
    public ActionResult<IEnumerable<ElectricityConsumption>> GetConsumptions()
    {
        return Ok(_consumptions);
    }

    [HttpGet("{id}")]
    public ActionResult<ElectricityConsumption> GetConsumption(int id)
    {
        var consumption = _consumptions.FirstOrDefault(c => c.Id == id);
        if (consumption == null)
        {
            return NotFound(new { message = "Consumption record not found" });
        }
        return Ok(consumption);
    }

    [HttpPost]
    public ActionResult<ElectricityConsumption> CreateConsumption([FromBody] ElectricityConsumption consumption)
    {
        if (consumption.ApartmentId <= 0 || consumption.QuantityKw <= 0)
        {
            return BadRequest(new { message = "Invalid data" });
        }
        consumption.Id = _consumptions.Count > 0 ? _consumptions.Max(c => c.Id) + 1 : 1;
        _consumptions.Add(consumption);
        return CreatedAtAction(nameof(GetConsumption), new { id = consumption.Id }, consumption);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateConsumption(int id, [FromBody] ElectricityConsumption updatedConsumption)
    {
        var consumption = _consumptions.FirstOrDefault(c => c.Id == id);
        if (consumption == null)
        {
            return NotFound(new { message = "Consumption record not found" });
        }

        consumption.ApartmentId = updatedConsumption.ApartmentId;
        consumption.Date = updatedConsumption.Date;
        consumption.QuantityKw = updatedConsumption.QuantityKw;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteConsumption(int id)
    {
        var consumption = _consumptions.FirstOrDefault(c => c.Id == id);
        if (consumption == null)
        {
            return NotFound(new { message = "Consumption record not found" });
        }

        _consumptions.Remove(consumption);
        return NoContent();
    }
}
