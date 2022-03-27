using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuestingEngine.Contract.Responses;
using QuestingEngine.Service.Commands;
using System;
using System.Threading.Tasks;

namespace QuestingEngine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public StateController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<PlayerStateResponse>> GetPlayerState(string playerId)
        {
            var response = await _mediator.Send(new GetPlayerStateCommand() { PlayerId = playerId });
            
            return response;
        }
    }
}
