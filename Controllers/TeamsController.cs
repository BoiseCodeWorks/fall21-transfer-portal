using System;
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
    public class TeamsController : ControllerBase
    {
      private readonly TeamsService _teamsService;
      private readonly PlayersService _playersService;

    public TeamsController(TeamsService teamsService, PlayersService playersService)
    {
      _teamsService = teamsService;
      _playersService = playersService;
    }

   [HttpGet]

   public ActionResult<List<Team>> GetAll()
   {
     try
     {
        return Ok(_teamsService.GetAll());
     }
     catch (System.Exception e)
     {
        return BadRequest(e.Message);
     }
   }

    [HttpGet("{teamId}")]

    public ActionResult<Team> GetById(int teamId)
    {
      try
      {
          return Ok(_teamsService.GetById(teamId));
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }


    // TODO actually get this to work but we need to be able to move players first
    [HttpGet("{teamId}/players")]

    public ActionResult<List<Player>> GetPlayersByTeam(int teamId)
    {
      try
      {
          return Ok(_playersService.GetPlayersByTeam(teamId));
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPost]

    public async Task<ActionResult<Team>> Post([FromBody] Team teamData)
    {
      try
      {
          Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
          // for node reference - req.body.creatorId = req.userInfo.id
          // FIXME NEVER TRUST THE CLIENT
          teamData.CreatorId = userInfo.Id;
          Team createdTeam = _teamsService.Post(teamData);
          createdTeam.Owner = userInfo;
          return createdTeam;
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpDelete("{teamId}")]

    public async Task<ActionResult<string>> RemoveTeam(int teamId)
    {
      try
      {
          Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
          _teamsService.RemoveTeam(teamId, userInfo.Id);
          return Ok("Team was delorted");
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }

  }
}