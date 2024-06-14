using DationBus.Api.Logics;
using DationBus.Business.Dtos;
using DationBus.Business.Services;
using Microsoft.AspNetCore.Mvc;
using CommonUtils.Models;
using CommonUtils.Wrappers;
using CommonUtils.Helpers;


namespace DationBus.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnicalController : ControllerBase
    {
        private readonly ITechnicalService _technicalService;
        private readonly IExceptionHelper _exceptionHelper;
        private readonly ILogger<TechnicalController> logger;

        public TechnicalController(ITechnicalService technicalService, IExceptionHelper exceptionHelper,
            ILogger<TechnicalController> logger)
        {
            _technicalService = technicalService;
            _exceptionHelper = exceptionHelper;
            this.logger = logger;   
        }

        [HttpPost]
        [Route("InsurancePayment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> CreateQueueMessage(CreateMessageDto createMessageDto)
        {
            try
            {
                await _technicalService.CreateQueueMessage(createMessageDto);
                return Ok(new Response<bool>(true));
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    ArgumentException => (ActionResult<bool>)BadRequest(new Response<bool>()
                    {
                        Errors = new string[] { _exceptionHelper.AppropriateMessage(ex.Message, ex, logger, Utils.EnableSwagger) }
                    }),
                    _ => (ActionResult<bool>)BadRequest(new Response<bool>()
                    {
                        Errors = new string[] { _exceptionHelper.AppropriateMessage(null, ex, logger, Utils.EnableSwagger) }
                    }),
                };
            }
        }
    }
}
