using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using QuestingEngine.Repository.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestingEngine.Repository
{
    public class QuestRepository : IQuestRepository
    {
        private readonly IMongoCollection<Quest> _questCollection;
        private readonly IMapper _mapper;
        public QuestRepository(IMongoDatabase mongoDatabase, IMapper mapper)
        {
            _questCollection = mongoDatabase.GetCollection<Quest>("quest");
            _mapper = mapper;
        }
        public async Task<ObjectId> CreateAsync(Model.Quest quest)
        {
            var questDataModel = _mapper.Map<Quest>(quest);
            questDataModel.Id = ObjectId.GenerateNewId();
            await _questCollection.InsertOneAsync(questDataModel);
            
            return questDataModel.Id;
        }

        public async Task<Model.Quest> GetAsync(ObjectId id)
        {
            var filter = Builders<Quest>.Filter.Eq(dm => dm.Id, id);
            var cursor = await _questCollection.FindAsync(filter, new FindOptions<Quest>() { Limit = 1 });
            var questDataModel = await cursor.FirstOrDefaultAsync();

            return _mapper.Map<Model.Quest>(questDataModel);
        }

        public async Task<List<Model.Quest>> GetAsync()
        {
            var cursor = await _questCollection.FindAsync(Builders<Quest>.Filter.Empty, new FindOptions<Quest>());
            var dataModels = await cursor.ToListAsync();

            return _mapper.Map<List<Model.Quest>>(dataModels);
        }

        public async Task UpdateAsync(Model.Quest quest)
        {
            var questDataModel = _mapper.Map<Quest>(quest);

            var filter = Builders<Quest>.Filter.Eq(dm => dm.Id, questDataModel.Id);
            var result = await _questCollection.FindOneAndReplaceAsync(filter, questDataModel, new FindOneAndReplaceOptions<Quest> { IsUpsert = false });
        }
    }
}
