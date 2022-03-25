using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QuestingEngine.Model.Configuration;
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
        public ProgressController()
        {
        }
    }
}
