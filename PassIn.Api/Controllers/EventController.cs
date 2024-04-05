using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.GetById;
using PassIn.Application.UseCases.Register;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;

namespace PassIn.Api.Controllers;

public class EventController : DefaultController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterEventJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody] RequestEventJson request)
    {

        var usecase = new RegisterEventUseCase();

        var evento = usecase.Execute(request);

        return Created(string.Empty, evento);

    }
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseEventJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public IActionResult ListarEventos(Guid id)
    {

        var useCase = new GetByIdUserCase();

        var response = useCase.Execute(id);

        return Ok(response);

    }
}
