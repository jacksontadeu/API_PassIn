using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Communication.Responses;
public class ResponseRegisterEventJson
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public  string Details { get; set; }
}
