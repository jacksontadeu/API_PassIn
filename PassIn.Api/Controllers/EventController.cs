using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.GetById;
using PassIn.Application.UseCases.Register;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;

public class EventController : DefaultController
{
    /// <summary>
    /// Método utilizado para cadastrar um evento.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterEventJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RequestEventJson request)
    {

        var usecase = new RegisterEventUseCase();

        var evento = await usecase.Execute(request);

        return Created(string.Empty, evento);

    }
    /// <summary>
    /// Método utilizado para listar evento determinado pelo ID do evento.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseEventJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ListarEventos(Guid id)
    {

        var useCase = new GetByIdUserCase();

        var response =  await useCase.Execute(id);

        return Ok(response);

    }
}
