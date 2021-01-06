using Core.Interfaces;
using Core.Managers;
using Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace VuetifyTest.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SystemUsersController : ControllerBase
    {
        private readonly SystemUserManager _systemUserManager;

        public SystemUsersController(SystemUserManager systemUserManager)
        {
            _systemUserManager = systemUserManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(SystemUserCreateViewModel systemUserToCreate)
        {
            IOperationResult<bool> createResult = await _systemUserManager.Create(systemUserToCreate);

            if (!createResult.Success)
            {
                return BadRequest(createResult.Message);
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("activate-account")]
        public async Task<IActionResult> Activate([FromQuery(Name = "email")] string email, [FromQuery(Name = "encriptedUsername")] string encriptedUsername)
        {
            IOperationResult<bool> createResult = await _systemUserManager.Activate(email, encriptedUsername);

            if (!createResult.Success)
            {
                return BadRequest(createResult.Message);
            }

            return Redirect("https://localhost:44336/");
        }
    }
}
