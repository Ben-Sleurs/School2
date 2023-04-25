using ExternalPeopleApp.Data;
using ExternalPeopleApp.Models;
using System.Net.Http;

namespace ExternalPeopleApp.Services
{
    public class PeopleApiRepository : IPeopleApiRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public PeopleApiRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public void Add(PersonEditModel person)
        {
            HttpClient client = _httpClientFactory.CreateClient(ApiConstants.PeopleApiHttpClientName);
            HttpResponseMessage response = client.PostAsJsonAsync("people", person).Result;
            if (!response.IsSuccessStatusCode)
            {
                //apifailure (no 200 response)
                throw new Exception("Could not save person");
            }
        }

        public IEnumerable<Person> GetAll()
        {
            HttpClient client = _httpClientFactory.CreateClient(ApiConstants.PeopleApiHttpClientName);
            HttpResponseMessage response = client.GetAsync("people").Result;
            if (response.IsSuccessStatusCode)
            {
                IList<Person> persons = response.Content.ReadAsAsync<IList<Person>>().Result;
                return persons;
            }
            else
            {
                return Enumerable.Empty<Person>();
            }
        }
    }
}
