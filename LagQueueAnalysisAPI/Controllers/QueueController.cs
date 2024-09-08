using LagQueueApplication.Interfaces;
using LagQueueApplication.Processings.Events;
using Microsoft.AspNetCore.Mvc;

namespace LagQueueAnalysisAPI.Controllers
{
    [Route("Queue")]
    public class QueueController : Controller
    {
        private readonly IExecuteProcessingEventService _executeProcessingEventService;
        private readonly IQueueQuery _queueQuery;

        public QueueController(IExecuteProcessingEventService executeProcessingEventService, 
                               IQueueQuery queueQuery)
        {
            _executeProcessingEventService = executeProcessingEventService;
            _queueQuery = queueQuery;
        }

        [Route("List")]
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                return Ok(_queueQuery.List());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
    }
}
