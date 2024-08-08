using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlassLewis.Company.Api.Repository.Entities
{
    public class Companies : EntityBase
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CompanyID { get; set; }
        [Column(TypeName = "NVARCHAR(50)")]
        public string Name { get; set; }
        [Column(TypeName = "NVARCHAR(50)")]
        public string Exchange { get; set; }
        [Column(TypeName = "NVARCHAR(500)")]
        public string Ticker { get; set; }
        [Column(TypeName = "NVARCHAR(10)")]
        public string Isin { get; set; }
        [Column(TypeName = "NVARCHAR(15)")]
        public string Website { get; set; }
    }
}
