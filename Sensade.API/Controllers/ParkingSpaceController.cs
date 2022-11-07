using Microsoft.AspNetCore.Mvc;
using Sensade.DataAccess.Repositories;
using Sensade.Shared.Models;

namespace Sensade.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParkingSpaceController : ControllerBase
{
    private readonly ILogger<ParkingSpaceController> _logger;
    private IParkingSpaceRepository _parkingSpaceRepository;

    public ParkingSpaceController(ILogger<ParkingSpaceController> logger, IParkingSpaceRepository parkingSpaceRepository)
    {
        _logger = logger;
        _parkingSpaceRepository = parkingSpaceRepository;
    }

    [HttpPost]
    public async Task<ActionResult<bool>> CreateAsync([FromBody] ParkingSpace entity)
    {
        try
        {
            var result = await _parkingSpaceRepository.Create(entity);

            if (result)
            {
                _logger.LogInformation("Parking space successfully created.");
            }
            else
            {
                _logger.LogInformation("Parking space failed to be created.");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Parking space was not created. {ex}");
            return BadRequest($"Parking area space not created. {ex}");
        }
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteAsync(int id)
    {
        try
        {
            var result = await _parkingSpaceRepository.Delete(id);

            if (result)
            {
                _logger.LogInformation($"Parking space with id={id} successfully deleted.");
            }
            else
            {
                _logger.LogInformation($"Parking space with id={id} doesn't exist or was already deleted");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Parking space was not deleted. {ex}");
            return BadRequest($"Parking space was not deleted. {ex}");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ParkingSpace>>> GetAll()
    {
        var parkingSpaces = await _parkingSpaceRepository.Get();

        return Ok(parkingSpaces);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ParkingSpace>> GetById(int id)
    {
        var parkingSpace = await _parkingSpaceRepository.Get(id);

        if (parkingSpace is null)
        {
            _logger.LogInformation($"Parking space with id={id} was not found.");
            return NotFound($"Parking space with id={id} was not found.");
        }
        else
        {
            _logger.LogInformation($"Parking space with id={id} successfully found.");
            return Ok(parkingSpace);
        }
    }

    [HttpPut]
    public async Task<ActionResult<bool>> Update([FromBody] ParkingSpace entity)
    {
        try
        {
            var result = await _parkingSpaceRepository.Update(entity);
            if (result)
            {
                _logger.LogInformation($"Parking space with id={entity.Id} was successfully updated.");
                return Ok(result);
            }
            else
            {
                _logger.LogInformation($"Parking space with id={entity.Id} failed to be updated.");
                return NotFound($"Parking space with id={entity.Id} was not found.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Parking space was not updated. {ex}");
            return BadRequest($"Parking area space was not updated. {ex}");
        }
    }
}
