using LagQueueApplication.Filters;
using LagQueueApplication.Interfaces;
using LagQueueApplication.Processings.Events;
using Microsoft.AspNetCore.Mvc;

namespace LagQueueAnalysisAPI.Controllers
{
    [Route("Messages")]
    public class MessagesController : Controller
    {
        private readonly IExecuteProcessingEventService _executeProcessingEventService;
        private readonly IMessageQuery _messageQuery;

        public MessagesController(IExecuteProcessingEventService executeProcessingEventService, 
                                  IMessageQuery messageQuery)
        {
            _executeProcessingEventService = executeProcessingEventService;
            _messageQuery = messageQuery;
        }


        [Route("List")]
        [HttpGet]
        public IActionResult List([FromQuery] MessageListFilter filter)
        {
            try
            {
                return Ok(_messageQuery.List(filter));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
