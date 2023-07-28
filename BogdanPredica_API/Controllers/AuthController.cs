using BogdanPredica_API.Models.Authentication;
using BogdanPredica_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BogdanPredica_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserRepository _userRepository;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserRepository userRepository, ILogger<AuthController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(AuthenticateRequest request)
        {
            var response = await _userRepository.Authenticate(request);
            if(response  == null)
            {
                _logger.LogWarning("Cineva cu user sau parola gresite vrea sa se logheze");
                return StatusCode((int)HttpStatusCode.BadRequest, "Username sau parola gresite");
            }
            return Ok(response);
        }
    }
}
