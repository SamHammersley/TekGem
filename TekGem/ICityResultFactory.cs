using System.Collections.Generic;

namespace CitySearch
{
    public interface ICityResultFactory
    {
        public ICityResult CreateResult(ICollection<string> NextLetters, ICollection<string> NextCities);
    }
}
