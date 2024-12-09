using System;
using System.Collections.Generic;
using ServiceContracts.DTO;
using ServiceContracts;
using Entites;
using Services;
using System.Data;
using System.Net;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCoreMock;
using AutoFixture;

namespace CRUDTests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;

        private readonly IFixture _fixture;

        public CountriesServiceTest()
        {
            var countriesInitialData = new List<Country>();


            //Creating Mock DbContext Class
            DbContextMock<ApplicationDbContext> dbContextMock = new DbContextMock<ApplicationDbContext>(

                new DbContextOptionsBuilder<ApplicationDbContext>().Options

                );

            //Creating a mock Dbset table that represents  countries table
            dbContextMock.CreateDbSetMock(temp => temp.Countries, countriesInitialData);



            var dbContext = dbContextMock.Object; // instance of DbContext




            _countriesService = new CountriesService(null);


            _fixture = new Fixture();

        }

        // when null is passed to AddCountryRequest , it should throw ArgumentNullException

        #region Add Country
        [Fact]
        public async Task AddCountry_NullCountry()
        {
            //Arrange
            CountryAddRequest? request = null;


            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
              {
                  //Act
                  await _countriesService.AddCountry(request);
              });

        }

        //when country name is null it should throw ArgumentException

        [Fact]
        public async Task AddCountry_CountryNameIsNull()
        {
            //Arrange

            CountryAddRequest? request = _fixture.Build<CountryAddRequest>().With(temp => temp.CountryName, null as string).Create();


            //Assert

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                await _countriesService.AddCountry(request);
            });

        }


        //When country is Duplicate Argument Exception
        [Fact]
        public async Task AddCountry_CountryNameIsDupicate()
        {
            //Arrange
            CountryAddRequest? request1 = _fixture.Build<CountryAddRequest>().With(temp => temp.CountryName, "USA").Create();

            CountryAddRequest? request2 = _fixture.Build<CountryAddRequest>().With(temp => temp.CountryName, "USA").Create();


            //Assert
            await Assert.ThrowsAsync<DuplicateNameException>(async () =>
            {
                //Act
                await _countriesService.AddCountry(request1);
                await _countriesService.AddCountry(request2);
            });


        }
        [Fact]

        //When you supply proper Country name , it should insert(add) the country to existing list of countries
        public async Task AddCountry_ProperCountryName()
        {
            //Arrange
            CountryAddRequest? request1 = _fixture.Build<CountryAddRequest>().With(temp => temp.CountryName, "UK").Create();


            //Act

            CountryResponse response = await _countriesService.AddCountry(request1);

            List<CountryResponse> countries_from_getallcountries = await _countriesService.GetAllCountries();

            //Assert
            Assert.True(response.CountryID != Guid.Empty);
            Assert.Contains(response, countries_from_getallcountries); // contains method calls equals method internally  and compares the address references of objects ,it cannot compare data inside the objects , so we override equals method in CountryResponse Class
        }
        #endregion

        //List of Countries should be empty by default (before adding any countries)

        #region GetAllCountries
        [Fact]
        public async Task GetAllCountries_EmptyList()
        {
            //Act
            List<CountryResponse> actual_list_countries_list = await _countriesService.GetAllCountries();

            //Assert

            Assert.Empty(actual_list_countries_list);
        }
        [Fact]
        //
        public async Task GetCountries_AddFewCountries()
        {
            //Arrange
            List<CountryAddRequest> countries_list_add_request = new List<CountryAddRequest>()
         {
             _fixture.Build<CountryAddRequest>().With(temp => temp.CountryName,"USA").Create()
,
             _fixture.Build<CountryAddRequest>().With(temp => temp.CountryName,"UAE").Create()

        };

            //Act

            List<CountryResponse> countries_list_from_add_countrylist = new List<CountryResponse>();

            foreach (CountryAddRequest request_countries in countries_list_add_request)
            {
                countries_list_from_add_countrylist.Add(await _countriesService.AddCountry(request_countries));
            }

            // Assert

            List<CountryResponse> actualCountriesResponseList = await _countriesService.GetAllCountries();
            foreach (CountryResponse expected_country in countries_list_from_add_countrylist)
            {
                Assert.Contains(expected_country, actualCountriesResponseList);
            }
        }
        #endregion

        #region GetCountryByCountryID
        [Fact]
        public async Task GetCountryBYCountryID_NotNull()
        {
            //Arrange
            Guid? countryId = null;

            //Act
            CountryResponse Countryname_from_GetCountryByCountryID = await _countriesService.GetCountryByCountryID(countryId);

            //Assert
            Assert.Null(Countryname_from_GetCountryByCountryID);
        }
        [Fact]
        //if we supply valid countryId ,it should return the matching country details as CountryResponse Object
        public async Task GetCountryBYCountryId_ValidCountryID()
        {
            CountryAddRequest request = _fixture.Build<CountryAddRequest>().With(temp => temp.CountryName, "USA").Create();

            //Arrange
            CountryResponse Country_from_add = await _countriesService.AddCountry(request);
            //Act
            CountryResponse Country_from_get = await _countriesService.GetCountryByCountryID(Country_from_add.CountryID);
            //Assert
            Assert.Equal(Country_from_add, Country_from_get);
        }
        #endregion

    }

}

