namespace GlassLewis.Company.Api.Model
{
    public class CompanyResponse
    {
        public CompanyStatus Status { get; set; }
        public string       Message { get; set; }
        public CompanyDto?   CompanyDetails { get; set; }
    }
}
