using System.Collections.Generic;
using CitySearch;
using Xunit;

namespace TekGem.Tests
{
    public class CityFinderTest
    {
        [Fact]
        public void TestSearch()
        {
            // mock
            var CityResultFactory = new CityResultFactory();

            var BangSearchString = "BANG";
            var BangExpectedResult = CityResultFactory.CreateResult(new string[] { "A", "K", "U" }, new string[] { "BANGALORE", "BANGKOK", "BANGUI" });

            var LaSearchString = "LA";
            var LaExpectedResult = CityResultFactory.CreateResult(new string[] { " ", " ", "G" }, new string[] { "LA PAZ", "LA PLATA", "LAGOS" });

            var ZeSearchString = "ZE";
            var ZeExpectedResult = CityResultFactory.CreateResult(new string[] { }, new string[] { });

            CityFinder Finder = new(CityResultFactory);

            Finder.AvailableCities = new SortedSet<string>(new List<string> { "BANDUNG", "BANGUI", "BANGKOK", "BANGALORE" });
            TestCitySearch(BangSearchString, Finder, BangExpectedResult);

            Finder.AvailableCities = new SortedSet<string>(new List<string> { "LA PAZ", "LA PLATA", "LAGOS", "LEEDS" });
            TestCitySearch(LaSearchString, Finder, LaExpectedResult);

            Finder.AvailableCities = new SortedSet<string>(new List<string> { "ZARIA", "ZHUGHAI", "ZIBO" });
            TestCitySearch(ZeSearchString, Finder, ZeExpectedResult);
        }

        private void TestCitySearch(string searchString, CityFinder cityFinder, ICityResult expectedResult)
        {
            ICityResult result = cityFinder.Search(searchString);
            Assert.Equal(expectedResult.NextCities, result.NextCities);
            Assert.Equal(expectedResult.NextLetters, result.NextLetters);
        }
    }
}
