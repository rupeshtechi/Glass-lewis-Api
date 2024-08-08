using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlassLewis.Company.Api.Repository.Entities
{
    public class Users : EntityBase
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        [Column(TypeName = "NVARCHAR(50)")]
        public string FirstName { get; set; }
        [Column(TypeName = "NVARCHAR(50)")]
        public string LastName { get; set; }
        [Column(TypeName = "NVARCHAR(500)")]
        public string Email { get; set; }
        [Column(TypeName = "NVARCHAR(50)")]
        public string Mobile { get; set; }
        [Column(TypeName = "NVARCHAR(15)")]
        public string DateOfBirth { get; set; }
        [Column(TypeName = "VARCHAR(1)")]
        public string Gender { get; set; }
        [Column(TypeName = "NVARCHAR(50)")]
        public string Password { get; set; }
        [Column(TypeName = "NVARCHAR(50)")]
        public string PasswordSalt { get; set; }
        public bool? IsApprove { get; set; }
        public bool? IsLockedOut { get; set; }
        [Column(TypeName = "datetime")]
        public Nullable<System.DateTime> LastLockedOutOn { get; set; }
        public int? FailedpasswordAttemptCount { get; set; }
    }
}
