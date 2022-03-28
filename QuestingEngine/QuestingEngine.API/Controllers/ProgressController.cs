using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QuestingEngine.Contract.Requests;
using QuestingEngine.Contract.Responses;
using QuestingEngine.Model.Configuration;
using QuestingEngine.Service.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QuestingEngine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressController : ControllerBase
    {
        private readonly QuestConfiguration _questConfiguration;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProgressController(IConfiguration configuration, IMediator mediator, IMapper mapper)
        {
            _questConfiguration = configuration.GetSection("QuestConfiguration").Get<QuestConfiguration>();
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<ActionResult<ProgressResponse>> UpdateProgress(ProgressRequest request)
        {
            var levelBonusRate = _questConfiguration?.LevelBonusRates?.FirstOrDefault(x => x.Level == request.PlayerLevel)?.Rate ?? 1;
            var questPointsEarned = Math.Floor((request.ChipAmountBet * (_questConfiguration?.RateFromBet ?? 1)) + (request.PlayerLevel * levelBonusRate));

            var command = _mapper.Map<UpdateProgressCommand>(request);
            command.QuestPointsEarned = (int)questPointsEarned;

            var response = await _mediator.Send(command);
            response.QuestPointsEarned = command.QuestPointsEarned;

            return response;
        }
    }
}
