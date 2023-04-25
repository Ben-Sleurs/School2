using ExternalPeopleApp.Data;

namespace ExternalPeopleApp.Services
{
    public class LocationApiRepository : ILocationApiRepository
    {
        public const string HttpClientName = "LocationApiClient";
        private readonly IHttpClientFactory _httpClientFactory;
        public LocationApiRepository(IHttpClientFactory httpClientFactory)
            {
            _httpClientFactory = httpClientFactory;
        }
        public IEnumerable<Location> GetAll()
        {
            HttpClient client = _httpClientFactory.CreateClient(ApiConstants.PeopleApiHttpClientName);
            client.DefaultRequestHeaders.Add("ApiKey","TestApiKey");
            HttpResponseMessage response = client.GetAsync("location").Result;
            if (response.IsSuccessStatusCode)
            {
                IList<Location> locations = response.Content.ReadAsAsync<IList<Location>>().Result;
                return locations;
            }
            else
            {
                return Enumerable.Empty<Location>();
            }
        }
    }
}
