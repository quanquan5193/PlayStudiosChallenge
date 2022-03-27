using MediatR;
using MongoDB.Bson;
using QuestingEngine.Contract.Responses;
using QuestingEngine.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuestingEngine.Service.Commands
{
    public class GetPlayerStateCommand : IRequest<PlayerStateResponse>
    {
        public string PlayerId { get; set; }
    }

    public class GetPlayerStateCommandHandler : IRequestHandler<GetPlayerStateCommand, PlayerStateResponse>
    {
        private readonly IPlayerRepository _playerRepository;

        public GetPlayerStateCommandHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }
        public async Task<PlayerStateResponse> Handle(GetPlayerStateCommand request, CancellationToken cancellationToken)
        {
            var player = await _playerRepository.GetAsync(ObjectId.Parse(request.PlayerId));
            var response = new PlayerStateResponse();
            response.TotalQuestPercentCompleted = (int)(((float)player.QuestPointsEarned / player.CurrentQuest.CompletedPoint) * 100);
            response.TotalQuestPercentCompleted = response.TotalQuestPercentCompleted < 100 ? response.TotalQuestPercentCompleted : 100;
            response.LastMilestoneIndexCompleted = player.CompletedMilestones.OrderBy(x => x.Index).LastOrDefault()?.Index;

            return response;
        }
    }
}
