using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Register;
using PassIn.Communication.Requests;

namespace PassIn.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    [HttpPost]
    public IActionResult Register([FromBody] RequestEventJson request)
    {
        try
        {
            var usecase = new RegisterEventUseCase();

            usecase.Execute(request);

            return Created();
        }
        catch (Exception ex) { }
    }
}
