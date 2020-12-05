using Core.Interfaces;
using Core.Managers;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace VuetifyTest.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public sealed class DashBoardController : ControllerBase
    {
        private readonly DashBoardManager _dashBoardManager;

        public DashBoardController(DashBoardManager dashBoardManager)
        {
            _dashBoardManager = dashBoardManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashBoardData()
        {
            IOperationResult<DashBoardViewModel> dashBoardDataResult = await _dashBoardManager.GetDashBoarData();

            if (!dashBoardDataResult.Success)
            {
                return BadRequest(dashBoardDataResult.Message);
            }

            return Ok(dashBoardDataResult.Entity);
        }
    }
}
