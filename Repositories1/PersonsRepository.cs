using Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using RepositryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositaries
{
	public class PersonsRepository : IPersonsRepositry
	{
		private readonly ApplicationDbContext _db;
		public PersonsRepository(ApplicationDbContext applicationDbContext)
		{
			_db = applicationDbContext;


		}
		public async Task<Person> AddPerson(Person person)
		{
			 _db.Persons.Add(person);
			await _db.SaveChangesAsync();
			return person;
		}

		public async Task<bool> DeletePerson(Guid personId)
		{
			Person deleteperson = (Person)_db.Persons.Where(temp => temp.PersonId == personId);
            _db.Persons.RemoveRange(deleteperson);
		   int rowdeleted= await _db.SaveChangesAsync();


			return rowdeleted>0;
		}

		public async Task<List<Person>> GetAllPersons()
		{
		  return	await _db.Persons.Include("Country_").ToListAsync();	
		}

		public async Task<Person?> GetPersonById(Guid? personId)
		{
		   return	await _db.Persons.Include("Country_").FirstOrDefaultAsync(temp=>temp.PersonId==personId);
		}

		public async  Task<List<Person>> GetPersonsFilters(Expression<Func<Person, bool>> predicate)
		{
		  return await 	_db.Persons.Include("Country_").Where(predicate).ToListAsync();	
		}

		public async Task<Person> UpdatePerson(Person person)
		{
		  Person? matching_person = await _db.Persons.Include("Country_").Where(temp => temp.PersonId == person.PersonId).FirstOrDefaultAsync();
			if (matching_person == null)
			{
				return person;	
			}
			matching_person.Name= person.Name;
			matching_person.Email= person.Name;
			matching_person.Gender= person.Name;
			matching_person.DateOFBirth= person.DateOFBirth;
			matching_person.Address= person.Name;
			matching_person.ReceiveNewsLetters= person.ReceiveNewsLetters;
			matching_person.TIN= person.TIN;
			matching_person.Country_.CountryName= person.Country_?.CountryName;

			await _db.SaveChangesAsync();
			return matching_person;
		}
	}
}
 