using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;
using System.Net.Mail;

namespace PassIn.Application.UseCases.RegisterAttendee;
public class RegisterAttendeeForEventUseCase
{
    private readonly PassInDbContext _context;

    public RegisterAttendeeForEventUseCase()
    {
        _context = new PassInDbContext();
    }

    public ResponseAttendeeJson Execute(Guid eventId, RequestRegisterEventJson request)
    {
        Validate(eventId, request);

        var entity = new Attendee
        {
            Email = request.Email,
            Name = request.Name,
            Event_Id = eventId,
            Created_At = DateTime.UtcNow,
        };
        _context.Attendees.Add(entity);
        _context.SaveChanges();
        return new ResponseAttendeeJson
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email

        };
    }
    private void Validate( Guid eventId, RequestRegisterEventJson request)
    {
        var eventExist = _context.Events.Find(eventId);
        if (eventExist is null)
            throw new NotFoundException("Evento não encontrado");
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ErrorOnValidationException("O nome não é válido");
        if (!ValidateEmail(request.Email))
            throw new ErrorOnValidationException("O email não é válido");

        var attendeeAlreadyRegistered = _context.Attendees.Any(attendee => attendee.Email.Equals(request.Email));
        if (attendeeAlreadyRegistered)
            throw new ErrorOnValidationException("Participante já logado no sistema");

        var attendeeLogged = _context.Attendees.Count(at=> at.Event_Id == eventId);
        if (attendeeLogged >= eventExist.Maximum_Attendees)
            throw new ErrorOnValidationException("Não há mais vagas, sala completa");
    }
    private bool ValidateEmail(string email)
    {
        try
        {
            new MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
        
    }
}
