using System;
using ServiceContracts.DTO;
using ServiceContracts;
using Entites;
using System.Data;
using System.Security.AccessControl;
using Services;
using System.ComponentModel.DataAnnotations;
using Services.Helpers;
using System.Security.Cryptography;
using Microsoft.VisualBasic.FileIO;
using ServiceContracts.Enums;
using System.Net.NetworkInformation;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using OfficeOpenXml;
using RepositryContracts;
using Microsoft.Extensions.Logging;


namespace Services
{
    public class PersonsService : IPersonService
    {
      private readonly   IPersonsRepositry _personsRepositry;

        private readonly ILogger<PersonsService> _logger;
        

		public PersonsService(IPersonsRepositry personsRepositry,ILogger<PersonsService> logger)
        {
            _personsRepositry = personsRepositry;

            _logger =logger;
        }


		
        public async Task<PersonResponse?> AddPerson(PersonAddRequest? personAddRequest)
        {
            //if (personAddRequest == null)
            //    throw new ArgumentNullException(nameof(personAddRequest));

            //if (_personService.Where(temp => temp.Name == personAddRequest.Name).Count() > 0)
            //    throw new ArgumentException(nameof(personAddRequest.Name));


            //Model Validation
            ValidationHelpers.ModelValidations(personAddRequest);


            // converting PersonAddRequest object to person Type
            Person? person = personAddRequest?.ToPerson();

            //generate new person id
            person.PersonId = Guid.NewGuid();

            //_db.Persons.Add(p); // adds model object to the Dbset
            await  _personsRepositry.AddPerson(person);

            return  person.ToPersonResponse();
        }

        public async Task<List<PersonResponse>> GetAllPersons()
        {                                          //SELECT * FROM Persons
                                                   //List<PersonResponse> list_of_persons = _db.Persons.ToList().
                                                   //    Select(person => personToPersonResponse(person)).ToList();

            //Adding Log infromation
            _logger.LogInformation("GetAllPersons of Persons Service");

            List<Person> persons = await _personsRepositry.GetAllPersons();

           List<PersonResponse> personResponses=  persons.Select(temp=>temp.ToPersonResponse()).ToList();

           
           // List<PersonResponse> list_of_persons = _db.GetAllPersons_Sp().Select(person =>person.ToPersonResponse()).ToList();

			return personResponses;
        }

        public async Task<PersonResponse?> GetPersonByPersonId(Guid? personId)
        {
            if (personId == null)
                return null;

            Person? person = await _personsRepositry.GetPersonById(personId);
           
            if (person == null)
                return null;


            PersonResponse? Person_from_Person_list = person.ToPersonResponse();

            return Person_from_Person_list;

        }

        public async Task<List<PersonResponse>> GetPersonsFilters(string? SearchBy, string? SearchString)
        {
			//Adding Log infromation
			_logger.LogInformation("GetPersonsFilters of Persons Service");


			List<Person> persons = SearchBy switch
            {
                nameof(PersonResponse.Name) => await _personsRepositry.GetPersonsFilters(temp => temp.Name.Contains(SearchString)),

                nameof(PersonResponse.Email) => await _personsRepositry.GetPersonsFilters(temp => temp.Email.Contains(SearchString)),

                nameof(PersonResponse.DateOFBirth) => await _personsRepositry.GetPersonsFilters(temp => temp.DateOFBirth.ToString().Contains(SearchString)),


                nameof(PersonResponse.Gender) => await _personsRepositry.GetPersonsFilters(temp => temp.Gender.Contains(SearchString  )),

                nameof(PersonResponse.Address) => await _personsRepositry.GetPersonsFilters(temp => temp.Address.Contains(SearchString)),


                nameof(PersonResponse.CountryID) => await _personsRepositry.GetPersonsFilters(temp => temp.Country_.CountryName.ToString().Contains(SearchString)),

                nameof(PersonResponse.ReceiveNewsLetters) => await _personsRepositry.GetPersonsFilters(temp => temp.ReceiveNewsLetters.ToString().Contains(SearchString)),


                _ => await _personsRepositry.GetAllPersons()

            };
            return persons.Select(temp=>temp.ToPersonResponse()).ToList();
        }


        public async Task< List<PersonResponse>> GetSortedPersons(List<PersonResponse> allpersons, string sortBy, SortOrderOptions sortorder)
        {
			//Adding Log infromation
			_logger.LogInformation("GetSortedPersons of Persons Service");


			if (string.IsNullOrEmpty(sortBy))
                return allpersons;


            List<PersonResponse> sorted_persons = (sortBy, sortorder) switch
            {
                (nameof(PersonResponse.Name), SortOrderOptions.ASC) => allpersons.OrderBy(temp => temp.Name).ToList(),
                (nameof(PersonResponse.Name), SortOrderOptions.DSC) => allpersons.OrderByDescending(temp => temp.Name).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.ASC) => allpersons.OrderBy(temp => temp.Email).ToList(),
                (nameof(PersonResponse.Email), SortOrderOptions.DSC) => allpersons.OrderByDescending(temp => temp.Email).ToList(),

                (nameof(PersonResponse.DateOFBirth), SortOrderOptions.ASC) => allpersons.OrderBy(temp => temp.DateOFBirth).ToList(),
                (nameof(PersonResponse.DateOFBirth), SortOrderOptions.DSC) => allpersons.OrderByDescending(temp => temp.DateOFBirth).ToList(),

                (nameof(PersonResponse.Address), SortOrderOptions.ASC) => allpersons.OrderBy(temp => temp.Address).ToList(),
                (nameof(PersonResponse.Address), SortOrderOptions.DSC) => allpersons.OrderByDescending(temp => temp.Address).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.ASC) => allpersons.OrderBy(temp => temp.Gender).ToList(),
                (nameof(PersonResponse.Gender), SortOrderOptions.DSC) => allpersons.OrderByDescending(temp => temp.Gender).ToList(),

                (nameof(PersonResponse.Age), SortOrderOptions.ASC) => allpersons.OrderBy(temp => temp.Age).ToList(),
                (nameof(PersonResponse.Age), SortOrderOptions.DSC) => allpersons.OrderByDescending(temp => temp.Age).ToList(),

                (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.ASC) => allpersons.OrderBy(temp => temp.ReceiveNewsLetters).ToList(),
                (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.DSC) => allpersons.OrderByDescending(temp => temp.ReceiveNewsLetters).ToList(),

                _ => allpersons // Default case

            } ;

            return sorted_persons;
        }

        public async Task<PersonResponse> updatePerson(PersonUpdateRequest? personUpdateRequest)
        {
            if(personUpdateRequest == null) 
                throw new ArgumentNullException(nameof(personUpdateRequest));   

            Person? matching_person =await _personsRepositry.GetPersonById(personUpdateRequest.PersonId);

            if(matching_person == null)
            {
                throw new ArgumentNullException("given person does not exists");
            }

            matching_person.Name = personUpdateRequest.Name;
            matching_person.Email = personUpdateRequest.Email;
            matching_person.DateOFBirth = personUpdateRequest.DateOFBirth;
            matching_person.CountryID = personUpdateRequest.CountryID;
            matching_person.Address = personUpdateRequest.Address;
            matching_person.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetters;

            //performing model validations
            ValidationHelpers.ModelValidations(personUpdateRequest);

		    await _personsRepositry.UpdatePerson(matching_person);

            return matching_person.ToPersonResponse();
        }

        public async Task<bool> DeletePerson(Guid? personId)
        {
           
            if(personId == null)
            {
              throw new ArgumentNullException(nameof(personId));
            }


             await _personsRepositry.DeletePerson(personId.Value);
            
            return true;    
        }

        public async Task<MemoryStream> GetPersonsCSV()
        {

             List<Person> people=   await _personsRepositry.GetAllPersons();
			//Get All Persons Data
			List<PersonResponse> personResponses =people.Select(temp=>temp.ToPersonResponse()).ToList();

            

              MemoryStream memoryStream  = new MemoryStream();    
              StreamWriter streamwriter = new StreamWriter(memoryStream);

            CsvConfiguration csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);

            CsvWriter csvwriter = new CsvWriter(streamwriter,csvConfiguration);

            //Writes the Heading as PersonId,Name,Email......... of PersonsResponse Properties
            // csvwriter.WriteHeader<PersonResponse>();

            csvwriter.WriteField(nameof(PersonResponse.PersonId));
            csvwriter.WriteField(nameof(PersonResponse.Name));
            csvwriter.WriteField(nameof(PersonResponse.Gender));
            csvwriter.WriteField(nameof(PersonResponse.Address));
            csvwriter.WriteField(nameof(PersonResponse.Country));
            //next recored

            foreach(PersonResponse person in personResponses)
            {
                csvwriter.WriteField(person.PersonId);
                csvwriter.WriteField(person.Name);
                csvwriter.WriteField(person.Gender);
                csvwriter.WriteField(person.Country);
            }

            await  csvwriter.WriteRecordsAsync(personResponses);

            memoryStream.Position= 0; //

            return memoryStream;    
        }

        public async Task<MemoryStream> GetPersonsExcel()  //for converting the data into excel sheets we use EPPlus Package
        {
            MemoryStream memoryStream = new MemoryStream();

            using (ExcelPackage excelPackage = new ExcelPackage(memoryStream))
            {
             ExcelWorksheet Worksheet =  excelPackage.Workbook.Worksheets.Add("PersonsSheet");

                Worksheet.Cells["A1"].Value = "Name";
                Worksheet.Cells["B1"].Value = "Email";
                Worksheet.Cells["C1"].Value = "Date of Birth";
                Worksheet.Cells["D1"].Value = "Gender";
                Worksheet.Cells["E1"].Value = "Address";
                Worksheet.Cells["F1"].Value = "Country";
                Worksheet.Cells["G1"].Value = "ReceiveNewsLetters";

                // styling the Headeer Cells
                using (ExcelRange headercells = Worksheet.Cells["A1:H1"])
                {
                    headercells.Style.Fill.PatternType=OfficeOpenXml.Style.ExcelFillStyle.DarkGray;
                    headercells.Style.Font.Bold=true;
                }



                    int row = 2;
				//Getting Persons List

				List<Person> people = await _personsRepositry.GetAllPersons();
				//Get All Persons Data
				List<PersonResponse> personResponses = people.Select(temp => temp.ToPersonResponse()).ToList();

				foreach (PersonResponse person in personResponses)
                {
                    Worksheet.Cells[row,1].Value= person.Name;
                    Worksheet.Cells[row,2].Value= person.Email;
                    Worksheet.Cells[row,3].Value= person.DateOFBirth?.ToString("dd-mm-yyyy");
                    Worksheet.Cells[row,4].Value= person.Gender;
                    Worksheet.Cells[row,5].Value= person.Address;
                    Worksheet.Cells[row,6].Value= person.Country;
                    Worksheet.Cells[row,7].Value= person.ReceiveNewsLetters;

                    row++;


                }

                Worksheet.Cells[$"A1:H{row}"].AutoFitColumns();

               await excelPackage.SaveAsync();
            }

            memoryStream.Position = 0;  
            return memoryStream;

          
        }
    }
}
            


           

