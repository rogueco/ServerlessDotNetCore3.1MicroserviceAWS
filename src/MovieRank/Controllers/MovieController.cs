using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieRank.Contract;
using MovieRank.Services;

namespace MovieRank.Controllers
{
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMoveRankService _moveRankService;

        public MovieController(IMoveRankService moveRankService)
        {
            _moveRankService = moveRankService;
        }

        [HttpGet]
        public async Task<IEnumerable<MovieResponse>> GetAllItemsFromDatabase()
        {
            IEnumerable<MovieResponse> results = await _moveRankService.GetAllItemsFromDatabase();
            return results;
        }

        [HttpGet]
        [Route("{userId}/{movieName}")]
        public async Task<MovieResponse> GetMovie(int userId, string movieName)
        {
            MovieResponse result = await _moveRankService.GetMovieFromDatabase(userId, movieName);
            return result;
        }

        [HttpGet]
        [Route("user/{userId}/rankedMovies/{movieName}")]
        public async Task<IEnumerable<MovieResponse>> GetUsersRankedMoviesByMovieTitle(int userId, string movieName)
        {
            var results = await _moveRankService.GetUsersRankedMoviesByMovieTitle(userId, movieName);
            return results;
        }

        [HttpPost]
        [Route("{userId}")]
        public async Task<IActionResult> AddMovie(int userId, [FromBody] MovieRankRequest movieRankRequest)
        {
            await _moveRankService.AddMovie(userId, movieRankRequest);
            return Ok();
        }

        [HttpPatch]
        [Route("{userId}")]
        public async Task<IActionResult> UpdateMovie(int userId, [FromBody] MovieUpdateRequest movieUpdateRequest)
        {
            await _moveRankService.UpdateMovie(userId, movieUpdateRequest);
            return Ok();
        }

        [HttpGet]
        [Route("{movieName}/ranking")]
        public async Task<MovieRankResponse> GetMoviesRanking(string movieName)
        {
            MovieRankResponse result = await _moveRankService.GetMovieRank(movieName);
            return result;
        }
    }
}