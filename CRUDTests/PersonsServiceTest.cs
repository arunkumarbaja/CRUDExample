using Entites;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using System;
using System.Data;
using System.Diagnostics.Metrics;
using System.Security.Cryptography.X509Certificates;
using Xunit;
using Xunit.Abstractions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCoreMock;
using AutoFixture;
using RepositryContracts;
using Moq;
using AutoFixture.Kernel;
using FluentAssertions;
namespace CRUDTests
{

    public class PersonsServiceTest
    {
        private readonly Mock<IPersonsRepositry>? _personRepositoryMock;

        private readonly IPersonsRepositry personsRepositry;

        private readonly IPersonService personService;

        private readonly ICountriesService? countriesService;

        private readonly ITestOutputHelper _testoutputHelper;

        private readonly IFixture _fixture;
        public PersonsServiceTest(ITestOutputHelper testOutPutHelper)
        {

            //creating Mock for Repository Methods

            personsRepositry =  _personRepositoryMock.Object;

            countriesService = new CountriesService(null);

            personService = new PersonsService(personsRepositry, null); ;

            _testoutputHelper = testOutPutHelper;

            _fixture = new Fixture();
        }

        private async Task<List<PersonResponse?>> CreatePersonsList()
        {
            CountryAddRequest countryAddRequest1 = _fixture.Create<CountryAddRequest>();
            CountryAddRequest countryAddRequest2 = _fixture.Create<CountryAddRequest>();


            CountryResponse? Country_response_1 = await countriesService.AddCountry(countryAddRequest1);
            CountryResponse? Country_response_2 = await countriesService.AddCountry(countryAddRequest2);

            //Auto Genareting the random data to the properties of Specific Object
            PersonAddRequest personAddRequest = _fixture.Build<PersonAddRequest>().With(temp => temp.Email, "exmple@gmail.com").Create();

            PersonAddRequest personAddRequest1 = _fixture.Build<PersonAddRequest>().With(temp => temp.Email, "exmple1@gmail.com").Create();

            PersonAddRequest personAddRequest2 = _fixture.Build<PersonAddRequest>().With(temp => temp.Email, "exmple2@gmail.com").Create();

            List<PersonAddRequest> request_list = new List<PersonAddRequest>();

            List<PersonResponse?> response_list_from_added_1 = new List<PersonResponse?>();

            foreach (PersonAddRequest request_person in request_list)
            {
                PersonResponse? person_Response = await personService.AddPerson(request_person);

                response_list_from_added_1.Add(person_Response);
            }

            return response_list_from_added_1;
        }

        #region Addperson
        [Fact]
        // if object of AddPersonRequest is null then it throws nullArgumentException
        public async Task AddPersons_NotNull()
        {
            //Arrange
            PersonAddRequest? request = null;

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
              {
                  //Act
                  await personService.AddPerson(request);

              });
        }

        // if name of person in Parameter object is null, then it should throw  Argument Exception
        [Fact]
        public async Task AddPersons_NameIsNotNull()
        {
            //Arrange
            PersonAddRequest? request = _fixture.Build<PersonAddRequest>().With(temp => temp.Name, null as string).Create();
            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
              {
                  //Act
                  await personService.AddPerson(request);
              });

        }


        // If we supply proper person details , it should insert into persons list
        [Fact]
        public async Task AddPerson_FullDetails_ToBeSuccessfull()
        {
            //Arrange
            PersonAddRequest personAddRequest = _fixture.Build<PersonAddRequest>().With(temp => temp.Email, "exmple@gmail.com").Create();

		

			Person person_from_add =  personAddRequest.ToPerson();

            

            //Mocking AddPersonMethod ---> if we supply value to the method it should return same value(Fixed object) always
             _personRepositoryMock?.Setup(temp => temp.AddPerson(It.IsAny<Person>())).ReturnsAsync(person_from_add);


            PersonResponse? personDetails_from_add = await personService.AddPerson(personAddRequest);

            person_from_add.PersonId.Should().NotBe(Guid.Empty);


          
        }
        #endregion



        #region GetPersonByPersonId

        [Fact]
        //if we suppay null as PersonId it should return null as PersonResponse
        public async Task GetPeronByPersonId_PersonIsNull()
        {
            Guid personId = Guid.Empty;

            PersonResponse? person_from_add = await personService.GetPersonByPersonId(personId);

            Assert.Null(person_from_add);

        }
        // if we supply Valid PersonId, it should return corresponding person details

        [Fact]
        public async Task GetPeronByPersonId_ValidPersonId()
        {
            CountryAddRequest? countryAddRequest = _fixture.Build<CountryAddRequest>().Create();

            CountryResponse? countryResponse = await countriesService.AddCountry(countryAddRequest);

            PersonAddRequest addRequest = _fixture.Build<PersonAddRequest>().With(temp => temp.Email, "exmple@gmail.com").Create();

            PersonResponse? person_from_add = await personService.AddPerson(addRequest);

            PersonResponse? person_from_get = await personService.GetPersonByPersonId(person_from_add?.PersonId);

            Assert.Equal(person_from_add, person_from_get);

        }

        #endregion




        #region  GetAllPersons
        //GetAllPersons should retuen empty list by Default
        [Fact]
        public async Task GetAllPersons_EmptyList()
        {
            List<PersonResponse>? PersonsList_from_get = await personService.GetAllPersons();

            //Assert
            Assert.Empty(PersonsList_from_get);
        }

        // first we add few persons and later we call GetAllPersons then it should return all persons thar were added

        [Fact]
        public async Task GetAllPersons_AddFewPersons()
        {
            //Arrange

            List<PersonResponse?> response_list_from_added = await CreatePersonsList();


            //printing Expected value from response_list_from_added
            _testoutputHelper.WriteLine("Expected  :");

            foreach (PersonResponse? p in response_list_from_added)
            {
                _testoutputHelper.WriteLine("Expected  :" + p?.ToString());
            }


            //Act
            List<PersonResponse> persons_from_Get = await personService.GetAllPersons();
            //Assert 
            foreach (PersonResponse? persons_response_from_list_added in response_list_from_added)
            {
                Assert.Contains(persons_response_from_list_added, persons_from_Get);
            }

            //printing Actual value from response_list_from_added
            _testoutputHelper.WriteLine("Actual  :");
            foreach (PersonResponse? p in persons_from_Get)
            {
                _testoutputHelper.WriteLine("Actual  :" + p.ToString());
            }


        }
        #endregion




        #region GetFillteredPersons
        // if Search Text is empty then it should return all persons
        [Fact]
        public async Task GetFilteredPersons_EmptySearchText()
        {
            PersonAddRequest p1 = new PersonAddRequest();

            //Arrange

            List<PersonResponse?> response_list_from_added = await CreatePersonsList();


            //printing Expected value from response_list_from_added
            _testoutputHelper.WriteLine("Expected  :");

            foreach (PersonResponse p in response_list_from_added)
            {
                _testoutputHelper.WriteLine("Expected  :" + p.ToString());
            }
            //Act
            List<PersonResponse> persons_from_Search = await personService.GetPersonsFilters(nameof(p1.Name), "");


            //Assert 
            foreach (PersonResponse? persons_response_from_list_added in response_list_from_added)
            {
                Assert.Contains(persons_response_from_list_added, persons_from_Search);
            }

            //printing Actual value from response_list_from_added
            _testoutputHelper.WriteLine("Actual  :");
            foreach (PersonResponse p in persons_from_Search)
            {
                _testoutputHelper.WriteLine("Actual  :" + p.ToString());
            }

        }



        // First we will add few persons and then;we will search based on name with sme string , then it should return all matching persons
        [Fact]
        public async Task GetFilteredPersons_SearchByPersonName()
        {
            PersonAddRequest p1 = new PersonAddRequest();
            //Arrange

            List<PersonResponse?> response_list_from_added = await CreatePersonsList();

            //printing Expected value from response_list_from_added
            _testoutputHelper.WriteLine("Expected  :");

            foreach (PersonResponse? p in response_list_from_added)
            {
                _testoutputHelper.WriteLine("Expected  :" + p.ToString());
            }

            //Act
            List<PersonResponse> persons_from_Search = await personService.GetPersonsFilters(nameof(p1.Name), "ar");

            //Assert 
            //printing Actual value from response_list_from_added
            _testoutputHelper.WriteLine("Actual  :");
            foreach (PersonResponse? p in persons_from_Search)
            {
                _testoutputHelper.WriteLine("Actual  :" + p.ToString());
            }

            foreach (PersonResponse? persons_response_from_list_added in response_list_from_added)
            {

                if (persons_response_from_list_added != null)
                {
                    if (persons_response_from_list_added.Name != null)
                    {
                        if (persons_response_from_list_added.Name.Contains("ar", StringComparison.OrdinalIgnoreCase))
                        {
                            Assert.Contains(persons_response_from_list_added, persons_from_Search);
                        }
                    }
                }
            }
        }

        #endregion



        #region  GetSortedPersons
        // When we sort on PersonName in Desc it should return the list of persons in Desceing Order
        [Fact]
        public async Task GetSortedPersons_BasedOnPersonNameInDesc()
        {
            PersonAddRequest p1 = new PersonAddRequest();

            //Arrange

            List<PersonResponse> response_list_from_added = await personService.GetAllPersons();

            response_list_from_added.OrderByDescending(temp => temp?.Name).ToList();

            //getting all persons in a list for passing to the GetSortedPersons Method
            List<PersonResponse> all_persons = await personService.GetAllPersons();


            //printing Expected value from response_list_from_added
            _testoutputHelper.WriteLine("Expected  :");

            foreach (PersonResponse p in response_list_from_added)
            {
                _testoutputHelper.WriteLine("Expected  :" + p.ToString());
            }
            //Act
            List<PersonResponse> persons_from_Sort = await personService.GetSortedPersons(all_persons, nameof(p1.Name), SortOrderOptions.DSC);




            //printing Actual value from response_list_from_added
            _testoutputHelper.WriteLine("Actual  :");
            foreach (PersonResponse p in persons_from_Sort)
            {
                _testoutputHelper.WriteLine("Actual  :" + p.ToString());
            }
            //Assert 
            for (int i = 0; i < response_list_from_added.Count; i++)
            {
                Assert.Equal(response_list_from_added[i], persons_from_Sort[i]);
            }
        }


        #endregion



        #region UpdatePerson

        //When we supply null as PersonAddRequest, it should throw ArgumentNullException

        [Fact]
        public async Task UpdatePerson_NullPersonUpdateRequest()
        {
            //Arrange
            PersonUpdateRequest? personUpdateRequest = null;

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
             {
                 //Act
                 await personService.updatePerson(personUpdateRequest);
             });
        }

        //When we supply Invalid PersonId  as PersonAddRequest, it should throw ArgumentException

        [Fact]
        public async Task UpdatePerson_InvalidPersonId()
        {
            //Arrange
            PersonUpdateRequest? personUpdateRequest = new PersonUpdateRequest()
            {
                PersonId = Guid.NewGuid(),

            };

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
             {
                 //Act
                 await personService.updatePerson(personUpdateRequest);
             });
        }

        //When we supply Person name is null  as PersonAddRequest, it should throw ArgumentException

        [Fact]
        public async Task UpdatePerson_PersonNameIsNull()
        {
            CountryAddRequest? countryAddRequest = new CountryAddRequest()
            {
                CountryName = "UK"
            };
            CountryResponse country_added = await countriesService.AddCountry(countryAddRequest);
            //Arrange
            PersonAddRequest person_Add_Request = new PersonAddRequest()
            {
                Name = "Arun kumar",
                DateOFBirth = DateTime.Parse("2002-02-22"),
                Address = "xxx",
                Email = "exmple@gmail.com"
            };

            PersonResponse? personResponse_from_add = await personService.AddPerson(person_Add_Request);

            PersonUpdateRequest? person_update_request = personResponse_from_add?.ToPersonUpdateRequest();

            person_update_request.Name = null;

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
              {
                  //Act
                  await personService.updatePerson(person_update_request);
              });
        }

        //updating the person details and that should return the updated results
        [Fact]
        public async Task UpdatePerson_PersonWithFullDetails()
        {
            CountryAddRequest? countryAddRequest = new CountryAddRequest()
            {
                CountryName = "UK"
            };
            CountryResponse country_added = await countriesService.AddCountry(countryAddRequest);
            //Arrange
            PersonAddRequest? personUpdateRequest = _fixture.Build<PersonAddRequest>().Create();

            PersonResponse? personResponse_from_add = await personService.AddPerson(personUpdateRequest);

            PersonUpdateRequest person_update_request = personResponse_from_add.ToPersonUpdateRequest();

            person_update_request.Name = "WILLIAM";
            person_update_request.Email = "wiliam@gmail.com";
            person_update_request.Address = "new York";

            PersonResponse person_response_from_update = await personService.updatePerson(person_update_request);

            PersonResponse? respose_from_GetPersonById = await personService.GetPersonByPersonId(personResponse_from_add?.PersonId);



            //Assert
            Assert.Equal(respose_from_GetPersonById, person_response_from_update);
        }


        #endregion

        #region DeletePerson
        [Fact]
        // if we supply valid PersonID then it should return true
        public async Task DeletePerson_ValidPersonID()
        {
            CountryAddRequest country = new CountryAddRequest()
            {
                CountryName = "UK"
            };
            CountryResponse? countryResponse = await countriesService.AddCountry(country);

            PersonAddRequest personAddRequest = _fixture.Build<PersonAddRequest>().Create();

            PersonResponse? personResponse_from_add = await personService.AddPerson(personAddRequest);

            bool isDeleted = await personService.DeletePerson(personResponse_from_add.PersonId);

            //Assert
            Assert.True(isDeleted);
        }
        [Fact]
        // if we supply Invalid PersonID then it should return false
        public async Task DeletePerson_InvalidPersonID()
        {


            bool isDeleted = await personService.DeletePerson(Guid.NewGuid());

            //Assert
            Assert.False(isDeleted);
        }
        #endregion


    }
}