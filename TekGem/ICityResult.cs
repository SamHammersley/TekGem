using System.Collections.Generic;

namespace CitySearch
{
    public interface ICityResult
    {
        public ICollection<string> NextLetters { get; set; }
        public ICollection<string> NextCities { get; set; }
    }
}
