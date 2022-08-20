using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Review.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Persistance
{
    public class ReviewService
    {
        private readonly IMongoCollection<Review.Domain.Entities.Review> _reviewCollection;

        public ReviewService(
            IOptions<ReviewDatabaseSettings> reviewDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                reviewDatabaseSettings.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(
                reviewDatabaseSettings.Value.DatabaseName);

            _reviewCollection = mongoDatabase.GetCollection<Review.Domain.Entities.Review>(
                reviewDatabaseSettings.Value.ReviewCollectionName);
        }

        public async Task<List<Review.Domain.Entities.Review>> GetAsync() =>
            await _reviewCollection.Find(_ => true).ToListAsync();

        public async Task<Review.Domain.Entities.Review?> GetAsync(string id) =>
            await _reviewCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Review.Domain.Entities.Review newComment) =>
            await _reviewCollection.InsertOneAsync(newComment);

        public async Task UpdateAsync(string id, Review.Domain.Entities.Review updatedComment) =>
            await _reviewCollection.ReplaceOneAsync(x => x.Id == id, updatedComment);

        public async Task RemoveAsync(string id) =>
            await _reviewCollection.DeleteOneAsync(x => x.Id == id);
    }
}
