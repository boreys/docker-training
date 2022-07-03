using TodoApp.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TodoApp.Services
{
    public class TodoService
    {
        private readonly IMongoCollection<TodoModel> todoCollection;

        // <snippet_ctor>
        public TodoService(
            IOptions<DatabaseSetting> databaseSettings)
        {
            var mongoUrl = new MongoUrl(databaseSettings.Value.ConnectionString);
            
            var mongoClient = new MongoClient(mongoUrl);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            todoCollection = mongoDatabase.GetCollection<TodoModel>(
                databaseSettings.Value.CollectionName);
        }
        // </snippet_ctor>

        public async Task<List<TodoModel>> GetAsync() =>
            await todoCollection.Find(_ => true).SortByDescending(x => x.Id).ToListAsync();

        public async Task<TodoModel?> GetAsync(string id) =>
            await todoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(TodoModel newBook) =>
            await todoCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, TodoModel updatedBook) =>
            await todoCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await todoCollection.DeleteOneAsync(x => x.Id == id);

    }
}
