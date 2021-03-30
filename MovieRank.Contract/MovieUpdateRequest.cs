namespace MovieRank.Contract
{
    public class MovieUpdateRequest
    {
        public string MovieName { get; set; }
        public int Ranking { get; set; }
    }
}