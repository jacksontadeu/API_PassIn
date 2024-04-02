using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.GetById;
using PassIn.Application.UseCases.Register;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;

namespace PassIn.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterEventJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody] RequestEventJson request)
    {
        try
        {
            var usecase = new RegisterEventUseCase();

            var evento = usecase.Execute(request);

            return Created(string.Empty, evento);
        }
        catch (PassInException ex)
        {
            return BadRequest(new ResponseErrorJson(ex.Message));
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorJson("Erro Desconhecido"));
        }

    }
    [HttpGet("{id}")]
    public IActionResult ListarEventos(Guid id)
    {
        try
        {
            var useCase = new GetByIdUserCase();

            var response = useCase.Execute(id);

            return Ok(response);
        }
        catch(PassInException ex)
        {
            return NotFound(new ResponseErrorJson(ex.Message));
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorJson("Erro Desconhecido"));
        }
    }
}
