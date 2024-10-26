using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models;
public class AuditableEntityDto
{
    public AuditableEntityDto()
    {
        
    }
    public AuditableEntityDto(AuditableEntity domain)
    {
        this.CreatedAt = domain.CreatedAt;
        this.UpdatedAt = domain.UpdatedAt;
    }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}