using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using MovieRank.Libs.Models;

namespace MovieRank.Libs.Repositories
{
    public class MovieRankRepository : IMovieRankRepository
    {
        private readonly DynamoDBContext _dynamoDbContext;

        public MovieRankRepository(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbContext = new DynamoDBContext(dynamoDbClient);
        }

        public async Task<IEnumerable<MovieDb>> GetAllItems()
        {
            return await _dynamoDbContext.ScanAsync<MovieDb>(new List<ScanCondition>()).GetRemainingAsync();
        }

        public async Task<MovieDb> GetMovie(int userId, string movieName)
        {
            return await _dynamoDbContext.LoadAsync<MovieDb>(userId, movieName);
        }

        public async Task<IEnumerable<MovieDb>> GetUsersRankedMoviesByMovieTitle(int userId, string movieTitle)
        {
            DynamoDBOperationConfig config = new DynamoDBOperationConfig
            {
                QueryFilter = new List<ScanCondition>
                {
                    new ScanCondition("MovieName", ScanOperator.BeginsWith, movieTitle)
                }
            };
            return await _dynamoDbContext.QueryAsync<MovieDb>(userId, config).GetRemainingAsync();
        }

        public async Task AddMovie(MovieDb movieDb)
        {
            await _dynamoDbContext.SaveAsync(movieDb);
        }

        public async Task UpdateMovie(MovieDb movieDb)
        {
            await _dynamoDbContext.SaveAsync(movieDb);
        }

        public async Task<IEnumerable<MovieDb>> GetMovieRank(string movieName)
        {
            DynamoDBOperationConfig config = new DynamoDBOperationConfig
            {
                IndexName = "MovieName-index"
            };

            return await _dynamoDbContext.QueryAsync<MovieDb>(movieName, config).GetRemainingAsync();
        }
    }
}