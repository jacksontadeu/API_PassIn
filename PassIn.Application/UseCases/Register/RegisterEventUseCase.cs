using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.Register;
public class RegisterEventUseCase
{
    public ResponseRegisterEventJson Execute(RequestEventJson request)
    {
        Validate(request);

        var context = new PassInDbContext();
        var evento = new Event
        {
            Title = request.Title,
            Details = request.Details,
            Maximum_Attendees = request.MaximumAttendees,
            Slug = request.Title.ToLower().Replace(" ", "-"),
        };

        context.Events.Add(evento);
        context.SaveChanges();
        return new ResponseRegisterEventJson
        {
            Id= evento.Id,
            Title = evento.Title,
            Details = evento.Details
            
        };
    }

    private void Validate(RequestEventJson request)
    {
        if (request.MaximumAttendees <= 0) throw new PassInException("Número de participantes inválido");
        if (string.IsNullOrWhiteSpace(request.Title)) throw new PassInException("O Título é invalido");
        if (string.IsNullOrWhiteSpace(request.Details)) throw new PassInException("O detalhe é inválido");
    }
}
