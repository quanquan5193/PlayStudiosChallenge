using AutoMapper;
using MediatR;
using MongoDB.Bson;
using QuestingEngine.Contract.Responses;
using QuestingEngine.Model.Configuration;
using QuestingEngine.Repository;
using QuestingEngine.Repository.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuestingEngine.Service.Commands
{
    public class UpdateProgressCommand : IRequest<ProgressResponse>
    {
        public string PlayerId { get; set; }
        public int PlayerLevel { get; set; }
        public int ChipAmountBet { get; set; }
        public int QuestPointsEarned { get; set; }
    }

    public class UpdateProgressCommandHandler : IRequestHandler<UpdateProgressCommand, ProgressResponse>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMilestoneRepository _milestoneRepository;
        private readonly IMapper _mapper;

        public UpdateProgressCommandHandler(IPlayerRepository playerRepository, IMilestoneRepository milestoneRepository, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _milestoneRepository = milestoneRepository;
            _mapper = mapper;
        }

        public async Task<ProgressResponse> Handle(UpdateProgressCommand request, CancellationToken cancellationToken)
        {
            var response = new ProgressResponse();
            var player = await _playerRepository.GetAsync(ObjectId.Parse(request.PlayerId));
            player.QuestPointsEarned += request.QuestPointsEarned;

            var milestones = await _milestoneRepository.GetAsync();
            UpdatePlayerCompletedMilestones(milestones, player);

            await _playerRepository.UpdateAsync(player);

            response.MilestoneCompleted = _mapper.Map<List<MilestoneResponse>>(player.CompletedMilestones);
            response.TotalQuestPercentCompleted = (int)(((float)player.QuestPointsEarned / player.CurrentQuest.CompletedPoint) * 100);
            response.TotalQuestPercentCompleted = response.TotalQuestPercentCompleted < 100 ? response.TotalQuestPercentCompleted : 100;

            return response;
        }

        private void UpdatePlayerCompletedMilestones(IEnumerable<Model.Milestone> milestones, Model.Player player)
        {
            var milestoneReached = milestones
                .Where(m => m.PointRequired <= player.QuestPointsEarned && 
                        (player.CompletedMilestones == null || 
                            (player.CompletedMilestones != null && !player.CompletedMilestones.Exists(x => x.Id == m.Id))
                        )).OrderBy(m => m.PointRequired);

            foreach (var milestone in milestoneReached)
            {
                if(player.CompletedMilestones == null)
                {
                    player.CompletedMilestones = new List<Model.Milestone>();
                }
                player.CompletedMilestones.Add(milestone);
                player.QuestPointsEarned += milestone.ChipAwarded;
            }
            while (milestoneReached?.Count() > 0)
            {
                UpdatePlayerCompletedMilestones(milestones, player);
            }
        }
    }
}
