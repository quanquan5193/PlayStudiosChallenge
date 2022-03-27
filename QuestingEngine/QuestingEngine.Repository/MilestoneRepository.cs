using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using QuestingEngine.Repository.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuestingEngine.Repository
{
    public class MilestoneRepository : IMilestoneRepository
    {
        private readonly IMongoCollection<Milestone> _milestoneCollection;
        private readonly IMapper _mapper;
        public MilestoneRepository(IMongoDatabase mongoDatabase, IMapper mapper)
        {
            _milestoneCollection = mongoDatabase.GetCollection<Milestone>("milestone");
            _mapper = mapper;
        }

        public async Task<ObjectId> CreateAsync(Model.Milestone milestone)
        {
            var milestoneDataModel = _mapper.Map<Milestone>(milestone);
            milestoneDataModel.Id = ObjectId.GenerateNewId();
            await _milestoneCollection.InsertOneAsync(milestoneDataModel);

            return milestoneDataModel.Id;
        }

        public async Task<Model.Milestone> GetAsync(ObjectId id)
        {
            var filter = Builders<Milestone>.Filter.Eq(dm => dm.Id, id);
            var cursor = await _milestoneCollection.FindAsync(filter, new FindOptions<Milestone>() { Limit = 1 });
            var milestoneDataModel = await cursor.FirstOrDefaultAsync();

            return _mapper.Map<Model.Milestone>(milestoneDataModel);
        }

        public async Task<List<Model.Milestone>> GetAsync()
        {
            var cursor = await _milestoneCollection.FindAsync(Builders<Milestone>.Filter.Empty, new FindOptions<Milestone>());
            var milestoneDataModel = await cursor.ToListAsync();

            return _mapper.Map<List<Model.Milestone>>(milestoneDataModel);
        }

        public async Task<List<Model.Milestone>> GetByMultipleId(IEnumerable<ObjectId> milestoneIds)
        {
            if (milestoneIds is null)
            {
                return null;
            }

            var filter = Builders<DataModel.Milestone>.Filter.In(r => r.Id, milestoneIds);
            var cursor = await _milestoneCollection.FindAsync(filter, new FindOptions<DataModel.Milestone>());
            var milestoneDataModel = await cursor.ToListAsync();

            var milestones = _mapper.Map<List<Model.Milestone>>(milestoneDataModel);
            return milestones;
        }

        public async Task UpdateAsync(Model.Milestone milestone)
        {
            var milestoneDataModel = _mapper.Map<Milestone>(milestone);

            var filter = Builders<Milestone>.Filter.Eq(dm => dm.Id, milestoneDataModel.Id);
            var result = await _milestoneCollection.FindOneAndReplaceAsync(filter, milestoneDataModel, new FindOneAndReplaceOptions<Milestone> { IsUpsert = false });
        }
    }
}
