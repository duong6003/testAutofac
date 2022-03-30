using System.ComponentModel.DataAnnotations.Schema;

namespace testAutofac.Data.Entities
{
    public class UserPermission
    {
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        public string? Code { get; set; }

        [ForeignKey("Code")]
        public virtual Permission? Permission { get; set; }
    }
}