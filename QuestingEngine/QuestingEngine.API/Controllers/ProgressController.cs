using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QuestingEngine.Contract.Requests;
using QuestingEngine.Contract.Responses;
using QuestingEngine.Model.Configuration;
using QuestingEngine.Service;
using QuestingEngine.Service.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestingEngine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressController : ControllerBase
    {
        private readonly QuestConfiguration questConfiguration;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProgressController(IConfiguration configuration, IMediator mediator, IMapper mapper)
        {
            questConfiguration = configuration.GetSection("QuestConfiguration").Get<QuestConfiguration>();
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<ActionResult<ProgressResponse>> UpdateProgress(ProgressRequest request)
        {
            var levelBonusRate = questConfiguration.LevelBonusRates?.FirstOrDefault(x => x.Level == request.PlayerLevel)?.Rate ?? 0;
            var questPointsEarned = Math.Floor((request.ChipAmountBet * questConfiguration.RateFromBet) + (request.PlayerLevel * levelBonusRate));

            var command = _mapper.Map<UpdateProgressCommand>(request);
            command.QuestPointsEarned = (int)questPointsEarned;

            var response = await _mediator.Send(command);
            response.QuestPointsEarned = command.QuestPointsEarned;

            return response;
        }
    }
}
