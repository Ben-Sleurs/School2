using System.Collections.Generic;
using PeopleApp.Shared.Entities;

namespace PeopleApp.Services
{
    public interface ILocationRepository
    {
        IEnumerable<Location> GetAll();
    }
}