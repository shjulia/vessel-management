using API.Commands.Vessels;
using API.Queries.Vessels;
using DomainModel.Entities.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReadModel.Vessels;

namespace API.Controllers;

[ApiController]
[Route("api/vessels")]
public class VesselsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Register([FromBody] RegisterVesselCommand command)
    {
        try
        {
            var id = await mediator.Send(command);
            return CreatedAtAction(nameof(Register), new { id }, id);
        }
        catch (Exception e)
        {
            return BadRequest(new {Error = e.Message});
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateVesselCommand command)
    {
        command.Id = id;
        try
        {
            await mediator.Send(command);
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(new {Error = e.Message});
        }
        return NoContent();
    }
    
    
    [HttpGet]
    public async Task<ActionResult<List<GetAllVesselsResponse>>> GetAll()
    {
        return await mediator.Send(new GetAllVesselsQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetVesselByIdResponse>> GetById(Guid id)
    {
        try
        {
            var vessel = await mediator.Send(new GetVesselByIdQuery(id));
            return Ok(vessel);
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(new {Error = e.Message});
        }
    }
}