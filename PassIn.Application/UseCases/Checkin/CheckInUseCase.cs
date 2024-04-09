using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;
using System.Data;
using System.Runtime.CompilerServices;

namespace PassIn.Application.UseCases.Checkin;
public class CheckInUseCase
{
    private readonly PassInDbContext _context;
    public CheckInUseCase()
    {
        _context = new PassInDbContext();
    }
    public async Task<ResponseRegisteredJson> ExecuteAsync(Guid attendeeId)
    {
        Validate(attendeeId);

        var entity = new CheckIn
        {
            Attendee_Id = attendeeId,
            Created_at = DateTime.UtcNow,
        };
        await _context.CheckIns.AddAsync(entity);
        await _context.SaveChangesAsync();
        return new ResponseRegisteredJson
        {
            Id = entity.Id,
        };
    }
    private void Validate(Guid attendeeId)
    {
        var attendee = _context.Attendees.Any(at=> at.Id == attendeeId);
        if (!attendee)
            throw new NotFoundException("Participante não encontarado");
        var attendeeChecked = _context.CheckIns.Any(ch=> ch.Attendee_Id == attendeeId);
        if(attendeeChecked)
            throw new ConflictException("Participante já realizou checkin nesse evento");

    }
}
