﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Infrastructure.Entities;
public class CheckIn
{
    public Guid Id { get; set; } =Guid.NewGuid();
    public DateTime Created_at { get; set; }
    public Guid Attendee_Id { get; set; }
}
