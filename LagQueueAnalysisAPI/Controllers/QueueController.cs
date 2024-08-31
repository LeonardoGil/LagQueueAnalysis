using LagQueueApplication.Interfaces;
using LagQueueApplication.Processings.Events;
using Microsoft.AspNetCore.Mvc;

namespace LagQueueAnalysisAPI.Controllers
{
    [Route("Queue")]
    public class QueueController : Controller
    {
        private readonly IExecuteProcessingEventService _executeProcessingEventService;

        public QueueController(IExecuteProcessingEventService executeProcessingEventService)
        {
            _executeProcessingEventService = executeProcessingEventService;
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register()
        {
            try
            {
                var processingId = await _executeProcessingEventService.On<RegisterQueueEvent, IRegisterQueueProcessingEvent>(new RegisterQueueEvent());

                return Ok(processingId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Messages/Register")]
        [HttpPost]
        public async Task<IActionResult> MessageRegister([FromBody]RegisterQueueMessagesEvent request)
        {
            try
            {
                var processingId = await _executeProcessingEventService.On<RegisterQueueMessagesEvent, IRegisterQueueMessagesProcessingEvent>(request);

                return Ok(processingId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
