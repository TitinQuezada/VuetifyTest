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
    }
}
