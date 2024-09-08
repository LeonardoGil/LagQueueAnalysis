using LagQueueApplication.Interfaces;
using LagQueueApplication.Processings.Events;
using Microsoft.AspNetCore.Mvc;

namespace LagQueueAnalysisAPI.Controllers
{
    [Route("Messages")]
    public class MessagesController : Controller
    {
        private readonly IExecuteProcessingEventService _executeProcessingEventService;

        public MessagesController(IExecuteProcessingEventService executeProcessingEventService)
        {
            _executeProcessingEventService = executeProcessingEventService;
        }


        [Route("Register")]
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
