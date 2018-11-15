using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebCoreTest.Controllers
{
	[Authorize]
	[ApiController]
	public abstract class BaseController : ControllerBase
	{
	}
}