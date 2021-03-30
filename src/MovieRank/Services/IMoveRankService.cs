using System.Collections.Generic;
using System.Threading.Tasks;
using MovieRank.Contract;

namespace MovieRank.Services
{
    public interface IMoveRankService
    {
        Task<IEnumerable<MovieResponse>> GetAllItemsFromDatabase();

        Task<MovieResponse> GetMovieFromDatabase(int userId, string movieName);

        Task<IEnumerable<MovieResponse>> GetUsersRankedMoviesByMovieTitle(int userId, string movieTitle);

        Task AddMovie(int userId, MovieRankRequest movieRankRequest);

        Task UpdateMovie(int userId, MovieUpdateRequest movieUpdateRequest);

        Task<MovieRankResponse> GetMovieRank(string movieName);
    }
}