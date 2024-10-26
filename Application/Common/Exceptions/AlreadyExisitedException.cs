using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions;
public class AlreadyExisitedException: Exception
{
    private string PropertyName { get; set; }
    public AlreadyExisitedException(string PropertyName,string? Message=null):base(Message)
    {
        this.PropertyName = PropertyName;
    }
}