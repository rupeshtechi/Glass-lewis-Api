using System.ComponentModel.DataAnnotations.Schema;

namespace GlassLewis.Company.Api.Repository.Entities
{
    public class EntityBase
    {
        [Column(TypeName = "bit")]
        public Nullable<System.Boolean> IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
