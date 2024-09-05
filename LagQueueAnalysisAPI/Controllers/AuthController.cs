using LagEnvironmentApplication.Interfaces;
using LagEnvironmentApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LagQueueAnalysisAPI.Controllers
{
    [Route("Auth")]
    public class AuthController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("Token")]
        public async Task<IActionResult> Token([FromBody] AuthenticateModel authenticateModel)
        {
            try
            {
                var token = await _authenticationService.Authenticate(authenticateModel);

                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
