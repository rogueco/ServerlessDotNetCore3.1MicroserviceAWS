using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieRank.Contract;
using MovieRank.Libs.Mappers;
using MovieRank.Libs.Models;
using MovieRank.Libs.Repositories;

namespace MovieRank.Services
{
    public class MovieRankService : IMoveRankService
    {
        private readonly IMovieRankRepository _movieRankRepository;
        private readonly IMapper _mapper;

        public MovieRankService(IMovieRankRepository movieRankRepository, IMapper mapper)
        {
            _movieRankRepository = movieRankRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieResponse>> GetAllItemsFromDatabase()
        {
            IEnumerable<MovieDb> response = await _movieRankRepository.GetAllItems();
            return _mapper.ToMovieContract(response);
        }

        public async Task<MovieResponse> GetMovieFromDatabase(int userId, string movieName)
        {
            MovieDb response = await _movieRankRepository.GetMovie(userId, movieName);
            return _mapper.ToMovieContract(response);
        }

        public async Task<IEnumerable<MovieResponse>> GetUsersRankedMoviesByMovieTitle(int userId, string movieTitle)
        {
            var response = await _movieRankRepository.GetUsersRankedMoviesByMovieTitle(userId, movieTitle);
            return _mapper.ToMovieContract(response);
        }

        public async Task AddMovie(int userId, MovieRankRequest movieRankRequest)
        {
            MovieDb movieDb = _mapper.ToMovieDbModel(userId, movieRankRequest);

            await _movieRankRepository.AddMovie(movieDb);
        }

        public async Task UpdateMovie(int userId, MovieUpdateRequest movieUpdateRequest)
        {
            var response = await _movieRankRepository.GetMovie(userId, movieUpdateRequest.MovieName);

            var movieDb = _mapper.ToMovieDbModel(userId, response, movieUpdateRequest);

            await _movieRankRepository.UpdateMovie(movieDb);
        }

        public async Task<MovieRankResponse> GetMovieRank(string movieName)
        {
            IEnumerable<MovieDb> response = await _movieRankRepository.GetMovieRank(movieName);

            double overallMovieRanking = Math.Round(response.Select(x => x.Ranking).Average());

            return new MovieRankResponse
            {
                MovieName = movieName,
                OverallRanking = overallMovieRanking
            };
        }
    }
}