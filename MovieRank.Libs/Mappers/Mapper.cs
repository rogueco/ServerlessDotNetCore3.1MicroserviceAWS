using System;
using System.Collections.Generic;
using System.Linq;
using MovieRank.Contract;
using MovieRank.Libs.Models;

namespace MovieRank.Libs.Mappers
{
    public class Mapper : IMapper
    {
        public IEnumerable<MovieResponse> ToMovieContract(IEnumerable<MovieDb> items)
        {
            return items.Select(ToMovieContract);
        }

        public MovieResponse ToMovieContract(MovieDb movie)
        {
            return new MovieResponse
            {
                Actors = movie.Actors,
                Descriptrion = movie.Description,
                Ranking = movie.Ranking,
                MovieName = movie.MovieName,
                TimeRanked = movie.RankedDateTime
            };
        }

        public MovieDb ToMovieDbModel(int userId, MovieRankRequest movieRankRequest)
        {
            return new MovieDb
            {
                Actors = movieRankRequest.Actors,
                UserId = userId,
                Description = movieRankRequest.Description,
                Ranking = movieRankRequest.Ranking,
                MovieName = movieRankRequest.MovieName,
                RankedDateTime = DateTime.UtcNow.ToString()
            };
        }


        public MovieDb ToMovieDbModel(int userId, MovieDb movieDbRequest, MovieUpdateRequest movieUpdateRequest)
        {
            return new MovieDb
            {
                UserId = movieDbRequest.UserId,
                Actors = movieDbRequest.Actors,
                Description = movieDbRequest.Description,
                Ranking = movieUpdateRequest.Ranking,
                MovieName = movieUpdateRequest.MovieName,
                RankedDateTime = DateTime.UtcNow.ToString()
            };
        }
    }
}