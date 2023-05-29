﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PeopleApp.Data;
using PeopleApp.Shared.Entities;

namespace PeopleApp.Services
{
    public class PersonDbRepository : IPersonRepository
    {
        private readonly DataContext _context;

        public PersonDbRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Person> GetAll()
        {
            return _context.People.Include(p => p.Department).Include(p => p.Location).ToList();
        }

        public Person GetById(long id)
        {
            return _context.People.Include(p => p.Department).Include(p => p.Location).FirstOrDefault(p => p.Id == id);
        }

        public void Add(Person person)
        {
            _context.People.Add(person);
            _context.SaveChanges();
        }

        public void Update(Person person)
        {
            _context.People.Update(person);
            _context.SaveChanges();
        }

        public void Delete(Person person)
        {
            _context.People.Remove(person);
            _context.SaveChanges();
        }
    }
}