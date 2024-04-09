using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases;
using PassIn.Application.UseCases.GetAllAttendees;
using PassIn.Application.UseCases.RegisterAttendee;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;

public class AttendeeController : DefaultController
{
    /// <summary>
    /// Método usado para registar um paricipante no evento determinado pelo Id do evento.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="eventId"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("{eventId}")]
    [ProducesResponseType(typeof(ResponseAttendeeJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]

    public async Task<IActionResult> RegisterAsync([FromBody] RequestRegisterEventJson request, [FromRoute] Guid eventId)
    {
        var usecase = new RegisterAttendeeForEventUseCase();

        var response = await usecase.ExecuteAsync(eventId, request);

        return Created(string.Empty, response);
    }
    /// <summary>
    /// Método usado para listar todos os participantes do evento determinado pelo ID do evento.
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{eventId}")]
    [ProducesResponseType(typeof(ResponseAttendeeJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public IActionResult GetAllAttendees([FromRoute] Guid eventId)
    {
        var usecase = new GetAllAttendeesByEventIdUseCase();

        var response = usecase.Execute(eventId);

        return Ok(response);

    }

}
