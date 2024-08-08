namespace GlassLewis.Company.Api.Model
{
    public class CompanyDto
    {
        public int      CompanyID { get; set; }
        public string   Name { get; set; }
        public string   Exchange { get; set; }
        public string   Ticker { get; set; }
        public string   Isin { get; set; }
        public string   Website { get; set; }
        public bool?    IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
