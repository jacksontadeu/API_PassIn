using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.RegisterAttendee;
using PassIn.Communication.Requests;

namespace PassIn.Api.Controllers;

public class AttendeeController : DefaultController
{
    [HttpPost]
    [Route("{eventId}")]

    public IActionResult Register([FromBody] RequestRegisterEventJson request, [FromRoute]Guid eventId)
    {
        var usecase = new RegisterAttendeeForEventUseCase();

        var response = usecase.Execute(eventId, request);

        return Created(string.Empty, response);
    }
}
