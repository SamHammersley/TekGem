using System;
using System.Collections.Generic;
using System.Linq;

namespace CitySearch
{
    public class CityFinder : ICityFinder
    {
        public SortedSet<string> AvailableCities { get; set; }

        private readonly ICityResultFactory _ResultFactory;

        public CityFinder(ICityResultFactory ResultFactory)
        {
            this._ResultFactory = ResultFactory;
        }

        // Finds the index of the first string which starts with the search string.
        // This function relies on the available cities being sorted; effectively it's a binary search.
        private int FindStartIndex(string searchString, int from, int to)
        {
            var left = from;
            var right = to;

            while (left <= right)
            {
                var midPoint = (left + right) / 2;
                var mid = AvailableCities.ElementAt(midPoint);

                var pre = midPoint == 0 ? null : AvailableCities.ElementAt(midPoint - 1);
                var comparison = searchString.CompareTo(mid.Substring(0, searchString.Length));

                if (comparison == 0 && (pre == null || !pre.StartsWith(searchString)))
                {
                    return midPoint;
                }
                else if (comparison > 0)
                {
                    left = midPoint + 1;
                }
                else
                {
                    right = midPoint - 1;
                }
            }

            return -1;
        }

        /// Finds the index of the last string which starts with the specified search string.
        /// This function relies on the available cities being sorted; effectively it's a binary search.
        private int FindEndIndex(string searchString, int from, int to)
        {
            var left = from;
            var right = to;

            while (left <= right)
            {
                var midPoint = (left + right) / 2;
                var mid = AvailableCities.ElementAt(midPoint);

                var post = midPoint == to ? null : AvailableCities.ElementAt(midPoint + 1);
                var comparison = searchString.CompareTo(mid.Substring(0, searchString.Length));

                if (comparison == 0 && (post == null || !post.StartsWith(searchString)))
                {
                    return midPoint;
                }
                else if (comparison < 0)
                {
                    right = midPoint - 1;
                }
                else
                {
                    left = midPoint + 1;
                }
            }

            return -1;
        }

        // should maybe cache searches and use them to reduce search space.
        public ICityResult Search(String searchString)
        {
            var From = 0;
            var To = AvailableCities.Count - 1;

            var Start = FindStartIndex(searchString, From, To);
            var End = FindEndIndex(searchString, Start, To);
            var Found = Start >= 0 && End >= 0;

            var Cities = !Found ? Array.Empty<string>() : AvailableCities.ToArray()[Start..(End + 1)];
            var Letters = Cities.Select(c => c.Substring(searchString.Length, 1)).ToArray();

            return _ResultFactory.CreateResult(Letters.ToHashSet(), Cities.ToHashSet());
        }
    }
}