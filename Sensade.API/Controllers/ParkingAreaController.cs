using Microsoft.AspNetCore.Mvc;
using Sensade.DataAccess.Repositories;
using Sensade.Shared.Models;

namespace Sensade.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParkingAreaController : ControllerBase
{
    private readonly ILogger<ParkingAreaController> _logger;
    private IParkingAreaRepository _parkingAreaRepository;

    public ParkingAreaController(ILogger<ParkingAreaController> logger, IParkingAreaRepository parkingAreaRepository)
    {
        _logger = logger;
        _parkingAreaRepository = parkingAreaRepository;
    }

    [HttpPost]
    public async Task<ActionResult<bool>> CreateAsync([FromBody] ParkingArea entity)
    {
        try
        {
            var result = await _parkingAreaRepository.Create(entity);

            if (result)
            {
                _logger.LogInformation("Parking area successfully created.");
            }
            else
            {
                _logger.LogInformation("Parking area failed to be created.");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Parking area was not created. {ex}");
            return BadRequest($"Parking area was not created. {ex}");
        }
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteAsync(int id)
    {
        try
        {
            var result = await _parkingAreaRepository.Delete(id);

            if (result)
            {
                _logger.LogInformation($"Parking area with id={id} successfully deleted.");
            }
            else
            {
                _logger.LogInformation($"Parking area with id={id} doesn't exist or was already deleted");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Parking area was not deleted. {ex}");
            return BadRequest($"Parking area was not deleted. {ex}");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ParkingArea>>> GetAll()
    {
        var parkingAreas = await _parkingAreaRepository.Get();

        return Ok(parkingAreas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ParkingArea>> GetById(int id)
    {
        var parkingArea = await _parkingAreaRepository.Get(id);

        if (parkingArea is null)
        {
            _logger.LogInformation($"Parking area with id={id} was not found.");
            return NotFound($"Parking area with id={id} was not found.");
        }
        else
        {
            _logger.LogInformation($"Parking area with id={id} successfully found.");
            return Ok(parkingArea);
        }
    }

    [HttpPut]
    public async Task<ActionResult<bool>> Update([FromBody] ParkingArea entity)
    {
        try
        {
            var result = await _parkingAreaRepository.Update(entity);
            if (result)
            {
                _logger.LogInformation($"Parking area with id={entity.Id} was successfully updated.");
                return Ok(result);
            }
            else
            {
                _logger.LogInformation($"Parking area with id={entity.Id} failed to be updated.");
                return NotFound($"Parking area with id={entity.Id} was not found.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Parking area was not updated. {ex}");
            return BadRequest($"Parking area was not updated. {ex}");
        }
    }
}
