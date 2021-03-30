using System.Collections.Generic;

namespace MovieRank.Contract
{
    public class MovieResponse
    {
        
        public string MovieName { get; set; }

        public string Descriptrion { get; set; }

        public List<string> Actors { get; set; }

        public int Ranking { get; set; }

        public string TimeRanked { get; set; }
    }
}