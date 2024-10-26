using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DomainArgumentException : Exception
    {
        public DomainArgumentException(string PropertyName,string? Message=null) :base(Message)
        {
           this.PropertyName = PropertyName;
        }
        public string PropertyName { get; set; }
    }
}
