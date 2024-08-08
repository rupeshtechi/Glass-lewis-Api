namespace GlassLewis.Company.Api.Model
{
    public class UserDto
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public enum TypeOfRoles : int
    {
        Member = 1,
        Admin = 2,
    }
}
