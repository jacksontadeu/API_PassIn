using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.GetById;
public class GetByIdUserCase
{
    private readonly PassInDbContext _context;
    public GetByIdUserCase()
    {
        _context = new PassInDbContext();
    }
    public async Task<ResponseEventJson> Execute(Guid id)
    {
        
        var evento = await _context.Events.Include(evento => evento.Attendees).FirstOrDefaultAsync(e=> e.Id == id);

        if (evento is null) throw new NotFoundException("Evento não encontrado");

        return new ResponseEventJson
        {
            Id = evento.Id,
            Title = evento.Title,
            Details = evento.Details,
            MaximumAttendees = evento.Maximum_Attendees,
             AttendeesAmount = evento.Attendees.Count(),
        };
    }
}
