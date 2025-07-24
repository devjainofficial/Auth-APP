namespace Philbor.Application.Features.ExternalAPI
{
    public class UserExternalAPIResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public AddressResult Address { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public CompanyResult Company { get; set; }
    }

    public class GeoResult
    {
        public string Lat { get; set; }
        public string Lng { get; set; }
    }

    public class AddressResult
    {
        public string Street { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public GeoResult Geo { get; set; }
    }

    public class CompanyResult
    {
        public string Name { get; set; }
        public string CatchPhrase { get; set; }
        public string Bs { get; set; }
    }
}
