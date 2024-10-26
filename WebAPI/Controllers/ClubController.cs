using Application.Commands;
using Application.Common.Models;
using Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/clubs")]
public class ClubController: BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<PaginationResponse<GameClubDto>>> GetAll([FromQuery] GetAllGameClubQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
    [HttpGet("{Id}")]
    public async Task<ActionResult<GameClubDto>> GetById(string Id,[FromQuery] GetGameClubByIdQuery query)
    {
        query.Id= Id;
        var result = await Mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<ActionResult<GameClubDto>> Create(CreateGameClubCommand command)
    {
        var result = await Mediator.Send(command);
        return Created(string.Empty, result);
    }
    [HttpPut("{Id}")]
    public async Task<IResult> Update(string Id, UpdateGameClubCommand command)
    {
        if (Id != command.Id) return Results.BadRequest();
        await Mediator.Send(command);
        return Results.NoContent();
    }
    [HttpDelete("{Id}")]
    public async Task<IResult> Delete(string Id)
    {
        await Mediator.Send(new DeleteGameClubCommand { Id = Id });
        return Results.NoContent();
    }


    [HttpGet("{Id}/events")]
    public async Task<ActionResult<PaginationResponse<GameClubEventDto>>> GetAllClubEvents(string Id, [FromQuery] GetAllGameClubEventQuery query)
    {
        query.ClubId = Id;
        var result = await Mediator.Send(query);
        return Ok(result);
    }
    [HttpPost("{Id}/events")]
    public async Task<ActionResult<GameClubEventDto>> CreateClubEvent(string Id,CreateGameClubEventCommand command)
    {
        command.ClubId = Id;
        var result = await Mediator.Send(command);
        return Created(string.Empty, result);
    }

}