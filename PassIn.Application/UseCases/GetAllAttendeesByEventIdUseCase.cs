using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases;
public class GetAllAttendeesByEventIdUseCase
{
    private readonly PassInDbContext _context;

    public GetAllAttendeesByEventIdUseCase()
    {
        _context = new PassInDbContext();
    }

    public ResponseAllAttendeesjson Execute(Guid eventId)
    {
        var evento = _context.Events.Include(ev => ev.Attendees).FirstOrDefault(ev => ev.Id == eventId);

        if (evento is null)
            throw new NotFoundException("Evento não localizado");

        return new ResponseAllAttendeesjson
        {
            Attendees = evento.Attendees.Select(at => new ResponseAttendeeJson
            {
                Id = at.Id,
                Name = at.Name,
                Email = at.Email,
                CreatedAt = at.Created_At,
                
            }).ToList(),
        };



    }
}
