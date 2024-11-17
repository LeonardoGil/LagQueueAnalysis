using LagQueueApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LagQueueAnalysisAPI.Controllers
{
    [Route("ProcessingEvent")]
    public class ProcessingEventController(IProcessingEventQuery processingEventQuery) : Controller
    {
        [HttpGet]
        [Route("ListLatest")]
        public IActionResult ListLatest()
        {
            try
            {
                var result = processingEventQuery.GetLastEvents();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            try
            {
                var result = processingEventQuery.GetById(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
