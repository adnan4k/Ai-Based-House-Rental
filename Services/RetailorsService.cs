using HouseStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
namespace HouseStoreApi.Services;

public class OwnerService
{
    private readonly IMongoCollection<Retailor> _OwnerCollection;

    public OwnerService(
        IOptions<HouseStoreDatabaseSettings> houseStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            houseStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            houseStoreDatabaseSettings.Value.DatabaseName);

        _OwnerCollection = mongoDatabase.GetCollection<Retailor>(
            houseStoreDatabaseSettings.Value.RetailorCollectionName);
    }

    public async Task<List<Retailor>> GetAsync() =>
        await _OwnerCollection.Find(_ => true).ToListAsync();

    public async Task<Retailor?> GetAsync(string id) =>
        await _OwnerCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Retailor newRetailor) =>
        await _OwnerCollection.InsertOneAsync(newRetailor);

    public async Task UpdateAsync(string id, Retailor updatedRetailor) =>
        await _OwnerCollection.ReplaceOneAsync(x => x.Id == id, updatedRetailor);

    public async Task RemoveAsync(string id) =>
        await _OwnerCollection.DeleteOneAsync(x => x.Id == id);
}