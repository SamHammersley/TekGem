using System.Collections.Generic;

namespace CitySearch
{
    public class CityResultFactory : ICityResultFactory
    {
        public ICityResult CreateResult(ICollection<string> NextLetters, ICollection<string> NextCities)
        {
            return new CityResult { NextLetters = NextLetters, NextCities = NextCities };
        }
    }
}
