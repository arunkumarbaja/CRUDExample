using System;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating person entity
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Adds a new person to list of persons
        /// </summary>
        /// <param name="personAddRequest"></param>
        /// <returns></returns>
        Task<PersonResponse?> AddPerson(PersonAddRequest? personAddRequest);
        /// <summary>
        /// returns all list of all persons from list of table
        /// </summary>
        /// <returns></returns>
      Task<List<PersonResponse>> GetAllPersons();
        /// <summary>
        /// returns person object based on PersonId
        /// </summary>
        /// <param name="personId">PersonId to retrive</param>
        /// <returns></returns>
      Task<PersonResponse?> GetPersonByPersonId(Guid? personId);

        /// <summary>
        /// returns all persons objects based on SearchBy and SearchString fields
        /// </summary>
        /// <param name="SearchBy"></param>
        /// <param name="SearchString"></param>
        /// <returns></returns>
      Task<List<PersonResponse>> GetPersonsFilters(string? SearchBy , string? SearchString);

        /// <summary>
        /// returns sorted list of persons
        /// </summary>
        /// <param name="allpersons">represents list of persons to sort</param>
        /// <param name="sortBy">Name of property upon which list should be sorted</param>
        /// <param name="sortorder">ASC or DSC </param>
        /// <returns>Returns list of persons after sorting</returns>
        Task<List<PersonResponse>> GetSortedPersons(List<PersonResponse> allpersons, string sortBy, SortOrderOptions sortorder);


        /// <summary>
        ///updates the spacified person details based on the given PersonId
        /// </summary>
        /// <param name="personUpdateRequest"> person details to update</param>
        /// <returns>Returns updated details of person</returns>
        Task<PersonResponse> updatePerson(PersonUpdateRequest? personUpdateRequest);

        Task<bool> DeletePerson(Guid? personId);

        Task<MemoryStream> GetPersonsCSV();

        Task<MemoryStream> GetPersonsExcel();
    }
}
