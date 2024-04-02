using PassIn.Communication.Requests;

namespace PassIn.Application.UseCases.Register;
public class RegisterEventUseCase
{
    public void Execute(RequestEventJson request)
    {
        Validate(request);
    }

    private void Validate(RequestEventJson request)
    {
        if (request.MaximumAttendees <= 0) throw new ArgumentException("Número de participantes inválido");
        if (string.IsNullOrWhiteSpace(request.Title)) throw new ArgumentException("O Título é invalido");
        if (string.IsNullOrWhiteSpace(request.Details)) throw new ArgumentException("O detalhe é inválido");
    }
}
