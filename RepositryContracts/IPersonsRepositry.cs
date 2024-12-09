using Entites;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositryContracts
{
	public interface IPersonsRepositry
	{
		/// <summary>
		/// Adds Persons to the data store
		/// </summary>
		/// <param name="person"></param>
		/// <returns> returns person object after adding</returns>
		Task<Person> AddPerson(Person person);
     
		/// <summary>
		/// Returns all persons in the datastore
		/// </summary>
		/// <returns> list of person objects from table</returns>
		Task<List<Person>> GetAllPersons();

		/// <summary>
		/// Returns a person object based on PersonId
		/// </summary>
		/// <param name="PersonId"></param>
		/// <returns>Object of person or null</returns>
		Task<Person?> GetPersonById(Guid?  personId);
		/// <summary>
		/// returns all person objects based in given expression
		/// </summary>
		/// <param name="predicate">Linq expression to check</param>
		/// <returns>all matching persons with given conditions</returns>

		Task<List<Person>> GetPersonsFilters(Expression<Func<Person, bool>> predicate);

		/// <summary>
		/// Updates the person  object based on given PersonId
		/// </summary>
		/// <param name="personId">PersonID to Update</param>
		/// <returns>Updated Person object</returns>
		Task<Person> UpdatePerson(Person person);

		/// <summary>
		/// deletes the person object based on PersonId
		/// </summary>
		/// <param name="personId">PersonID to Delete</param>
		/// <returns>true if deletion is sucessfull or false</returns>
		Task<bool> DeletePerson (Guid personId);
	}
}
