using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.Register;
public class RegisterEventUseCase
{
    private readonly PassInDbContext _context;
    public RegisterEventUseCase()
    {
        _context = new PassInDbContext();
    }
    public async Task<ResponseRegisterEventJson> Execute(RequestEventJson request)
    {
        Validate(request);
        
        var evento = new Event
        {
            Title = request.Title,
            Details = request.Details,
            Maximum_Attendees = request.MaximumAttendees,
            Slug = request.Title.ToLower().Replace(" ", "-"),
        };

        await _context.Events.AddAsync(evento);
        await _context.SaveChangesAsync();
        return new ResponseRegisterEventJson
        {
            Id= evento.Id,
            Title = evento.Title,
            Details = evento.Details
            
        };
    }

    private void Validate(RequestEventJson request)
    {
        if (request.MaximumAttendees <= 0) throw new ErrorOnValidationException("Número de participantes inválido");
        if (string.IsNullOrWhiteSpace(request.Title)) throw new ErrorOnValidationException("O Título é invalido");
        if (string.IsNullOrWhiteSpace(request.Details)) throw new ErrorOnValidationException("O detalhe é inválido");
    }
}
