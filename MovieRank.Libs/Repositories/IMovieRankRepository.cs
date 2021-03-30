using System.Collections.Generic;
using System.Threading.Tasks;
using MovieRank.Libs.Models;

namespace MovieRank.Libs.Repositories
{
    public interface IMovieRankRepository
    {
        Task<IEnumerable<MovieDb>> GetAllItems();
        Task<MovieDb> GetMovie(int userId, string movieName);

        Task<IEnumerable<MovieDb>> GetUsersRankedMoviesByMovieTitle(int userId, string movieTitle);

        Task AddMovie(MovieDb movieDb);
        
        Task UpdateMovie(MovieDb movieDb);

        Task<IEnumerable<MovieDb>> GetMovieRank(string movieName);
    }
}