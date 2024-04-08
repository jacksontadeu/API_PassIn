using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Application.UseCases.GetById;
public class GetByIdUserCase
{
    public ResponseEventJson Execute(Guid id)
    {
        var context = new PassInDbContext();

        var evento = context.Events.Include(evento => evento.Attendees).FirstOrDefault(e=> e.Id == id);

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
