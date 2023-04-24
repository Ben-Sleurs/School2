using ExternalPeopleApp.Data;
using ExternalPeopleApp.Models;

namespace ExternalPeopleApp.Services;

public class PeopleApiRepository:IPeopleRepository
{
    private readonly IHttpClientFactory _httpClientFactory;

    public PeopleApiRepository(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
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

    public IEnumerable<Person> GetById()
    {
        throw new NotImplementedException();
    }

    public void Add(PersonEditModel person)
    {
        HttpClient client = _httpClientFactory.CreateClient(ApiConstants.PeopleApiHttpClientName);

        HttpResponseMessage response = client.PostAsJsonAsync("people", person).Result;

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Could not save person");
        }
    }

    public void Update(PersonEditModel person)
    {
        throw new NotImplementedException();
    }

    public void Delete(Person person)
    {
        throw new NotImplementedException();
    }
}