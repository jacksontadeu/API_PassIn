using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Checkin;
using PassIn.Communication.Responses;
using PassIn.Infrastructure;

namespace PassIn.Api.Controllers;

public class CheckInController : DefaultController
{
    
    [HttpPost]
    [Route("{attendeeId}")]
    [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]

    public IActionResult CheckIn([FromRoute]Guid attendeeId)
    {
        var usecase = new CheckInUseCase();

        var response = usecase.Execute(attendeeId);

        return Created(string.Empty, response);
    }

}
