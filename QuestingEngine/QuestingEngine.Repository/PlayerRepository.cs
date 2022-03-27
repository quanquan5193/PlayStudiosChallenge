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
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IMongoCollection<Player> _playerCollection;
        private readonly IQuestRepository _questRepository;
        private readonly IMilestoneRepository _milestoneRepository;
        private readonly IMapper _mapper;
        public PlayerRepository(IMongoDatabase mongoDatabase, IMapper mapper, IQuestRepository questRepository, IMilestoneRepository milestoneRepository)
        {
            _playerCollection = mongoDatabase.GetCollection<Player>("player");
            _questRepository = questRepository;
            _milestoneRepository = milestoneRepository;
            _mapper = mapper;
        }

        public async Task<ObjectId> CreateAsync(Model.Player player)
        {
            var playerDataModel = _mapper.Map<Player>(player);
            playerDataModel.Id = ObjectId.GenerateNewId();
            playerDataModel.CompletedMilestones = player.CompletedMilestones?.Select(x => x.Id);
            await _playerCollection.InsertOneAsync(playerDataModel);

            return playerDataModel.Id;
        }

        public async Task<Model.Player> GetAsync(ObjectId id)
        {
            var filter = Builders<Player>.Filter.Eq(dm => dm.Id, id);
            var cursor = await _playerCollection.FindAsync(filter, new FindOptions<Player>() { Limit = 1 });
            var playerDataModel = await cursor.FirstOrDefaultAsync();
            var player = _mapper.Map<Model.Player>(playerDataModel);
            player.CurrentQuest = await _questRepository.GetAsync(playerDataModel.CurrentQuest);
            player.CompletedMilestones = await _milestoneRepository.GetByMultipleId(playerDataModel.CompletedMilestones);

            return player;
        }

        public async Task<List<Model.Player>> GetAsync()
        {
            var cursor = await _playerCollection.FindAsync(Builders<Player>.Filter.Empty, new FindOptions<Player>());
            var dataModels = await cursor.ToListAsync();

            return _mapper.Map<List<Model.Player>>(dataModels);
        }

        public async Task UpdateAsync(Model.Player player)
        {
            var playerDataModel = _mapper.Map<Player>(player);

            var filter = Builders<Player>.Filter.Eq(dm => dm.Id, playerDataModel.Id);
            var result = await _playerCollection.FindOneAndReplaceAsync(filter, playerDataModel, new FindOneAndReplaceOptions<Player> { IsUpsert = false });
        }
    }
}
