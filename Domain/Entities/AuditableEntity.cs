using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class AuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
