using PassIn.Communication.Requests;
using PassIn.Exceptions;

namespace PassIn.Application.UseCases.Register;
public class RegisterEventUseCase
{
    public void Execute(RequestEventJson request)
    {
        Validate(request);
    }

    private void Validate(RequestEventJson request)
    {
        if (request.MaximumAttendees <= 0) throw new PassInException("Número de participantes inválido");
        if (string.IsNullOrWhiteSpace(request.Title)) throw new PassInException("O Título é invalido");
        if (string.IsNullOrWhiteSpace(request.Details)) throw new PassInException("O detalhe é inválido");
    }
}
