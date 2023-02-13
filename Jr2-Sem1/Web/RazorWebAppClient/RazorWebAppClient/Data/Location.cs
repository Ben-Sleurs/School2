namespace RazorWebAppClient.Data
{
    public class Location
    {
        public Location(int locationId, string postCode, string city)
        {
            LocationId = locationId;
            PostCode = postCode;
            City = city;
        }

        public int LocationId { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
    }
}
