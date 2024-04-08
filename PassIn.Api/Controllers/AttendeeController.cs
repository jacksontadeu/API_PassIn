using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases;
using PassIn.Application.UseCases.RegisterAttendee;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;

public class AttendeeController : DefaultController
{
    [HttpPost]
    [Route("{eventId}")]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]

    public IActionResult Register([FromBody] RequestRegisterEventJson request, [FromRoute] Guid eventId)
    {
        var usecase = new RegisterAttendeeForEventUseCase();

        var response = usecase.Execute(eventId, request);

        return Created(string.Empty, response);
    }
    [HttpGet]
    [Route("{eventId}")]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public IActionResult GetAllAttendees([FromRoute] Guid eventId)
    {
        var usecase = new GetAllAttendeesByEventIdUseCase();

        var response = usecase.Execute(eventId);

        return Ok(response);

    }

}
