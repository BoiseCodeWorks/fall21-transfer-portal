using System.Collections.Generic;
using System.Threading.Tasks;
using cfbTransferPortal.Models;
using cfbTransferPortal.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cfbTransferPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
      private readonly PlayersService _playersService;

    public PlayersController(PlayersService playersService)
    {
      _playersService = playersService;
    }

    [HttpGet]

    public ActionResult<List<Player>> GetAll()
    {
      try
      {
           return Ok(_playersService.GetAll());
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }

    [HttpGet("{playerId}")]

    public ActionResult<Player> GetById(int playerId)
    {
      try
      {
          return Ok(_playersService.GetById(playerId));
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPut("{playerId}")]

    public async Task<ActionResult<Player>> Update(int playerId, [FromBody] Player updatedPlayer) 
    {
      try
      {
          Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
          updatedPlayer.Id = playerId;
          return _playersService.Update(updatedPlayer, userInfo.Id);
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
  }
}