﻿
using Microsoft.AspNetCore.Mvc;

namespace BattleCalculator.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public abstract class BaseApiController : ControllerBase { }
}
