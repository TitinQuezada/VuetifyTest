using Core.Interfaces;
using Core.Managers;
using Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace VuetifyTest.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly AuthenticationManager _authenticationManager;

        public AuthenticationsController(AuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthenticationRequest authenticationRequest)
        {
            IOperationResult<AuthenticationViewModel> loginResult = await _authenticationManager.Login(authenticationRequest);

            if (!loginResult.Success)
            {
                return BadRequest(loginResult.Message);
            }

            return Ok(loginResult.Entity);
        }
    }
}
