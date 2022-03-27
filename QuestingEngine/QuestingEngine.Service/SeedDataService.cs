using QuestingEngine.Model;
using QuestingEngine.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestingEngine.Service
{
    public class SeedDataService : ISeedDataService
    {
        private readonly IMilestoneRepository _milestoneRepository;
        private readonly IQuestRepository _questRepository;
        private readonly IPlayerRepository _playerRepository;

        public SeedDataService(IMilestoneRepository milestoneRepository, IQuestRepository questRepository, IPlayerRepository playerRepository)
        {
            _milestoneRepository = milestoneRepository;
            _questRepository = questRepository;
            _playerRepository = playerRepository;
        }
        public async Task InitializeSeedData()
        {
            var milestones = await _milestoneRepository.GetAsync();
            var questses = await _questRepository.GetAsync();
            var players = await _playerRepository.GetAsync();

            if(milestones.Any() || questses.Any() || players.Any())
            {
                return;
            }

            var milestone1 = new Milestone() { Index = 1, ChipAwarded = 50, PointRequired = 50 };
            var milestone2 = new Milestone() { Index = 2, ChipAwarded = 100, PointRequired = 100 };
            var milestone3 = new Milestone() { Index = 3, ChipAwarded = 150, PointRequired = 150 };

            milestone1.Id = await _milestoneRepository.CreateAsync(milestone1);
            milestone2.Id = await _milestoneRepository.CreateAsync(milestone2);
            milestone3.Id = await _milestoneRepository.CreateAsync(milestone3);

            var quest = new Quest() {
                Name = "First mission",
                Milestones = new List<Milestone>() { milestone1, milestone2, milestone3 },
                CompletedPoint = 150
            };

            quest.Id = await _questRepository.CreateAsync(quest);

            var player = new Player()
            {
                Name = "John",
                Level = 1,
                QuestPointsEarned = 0,
                CurrentQuest = quest
            };

            await _playerRepository.CreateAsync(player);
        }
    }
}
