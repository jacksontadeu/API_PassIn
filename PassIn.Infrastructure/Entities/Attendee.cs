﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Infrastructure.Entities;
public class Attendee
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Guid Event_Id { get; set; }
    public DateTime Created_At { get; set; } = DateTime.UtcNow;
    public CheckIn? Checkin { get; set; }
}
