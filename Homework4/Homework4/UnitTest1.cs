using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceReference1;

[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]
namespace Homework4
{
    [TestClass]
    public class UnitTest1
    {
        private static CountryInfoServiceSoapTypeClient countryAPI = null;

        [TestInitialize]
        public void TestInit()
        {
            countryAPI = new CountryInfoServiceSoapTypeClient(CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);
        }

        //TODO
        //Create a test method to validate the return of ‘ListOfCountryNamesByCode()’ API is by Ascending Order of Country Code​  
        [TestMethod]
        public void IsListOfCountryNamesByCodeAscending()
        {
            var countryList = countryAPI.ListOfCountryNamesByCode();
            var ascOrder = countryList.OrderBy(x => x.sISOCode);
            Assert.IsTrue(countryList.SequenceEqual(ascOrder), "List Of Country Names By Code is not  Ascending");
        }

        //Create a test method to validate passing of invalid Country Code to ‘CountryName()’ API returns ‘Country not found in the database’​
        [TestMethod]
        public void IsCountryCodeInvalid()
        {
            var country = countryAPI.CountryName("testInvalid");
            Assert.AreEqual("Country not found in the database", country, "Country is available in the list and valid");
        }

        //Create a test method that gets the last entry from ‘ListOfCountryNamesByCode()’ API and pass the return value Country Code to ‘CountryName()’ API then validate the Country Name from both API is the same​
        [TestMethod]
        public void GetLastEntryandCompare()
        {
            var countryList = countryAPI.ListOfCountryNamesByCode();
            var country = countryList.Last();
            var countryName = countryAPI.CountryName(country.sISOCode);
            Assert.AreEqual(country.sName, countryName, "Country do not match");
        }
    }
}