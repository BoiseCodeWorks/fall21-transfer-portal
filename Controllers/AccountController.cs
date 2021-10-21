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
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly PlayersService _playersService;
        private readonly TeamsService _teamsService;

        public AccountController(AccountService accountService, PlayersService playersService, TeamsService teamsService)
        {
            _accountService = accountService;
            _playersService = playersService;
            _teamsService = teamsService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Account>> Get()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_accountService.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize]
        [HttpGet("teams")]
        public async Task<ActionResult<List<Team>>> GetTeamsByAccount()
        {
            try
            {
               Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
               return Ok(_teamsService.GetTeamsByAccount(userInfo.Id));
            }
            catch (System.Exception e)
            {
              return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet("players")]
    
        public async Task<ActionResult<List<Player>>> GetPlayersByAccount()
        {
            try
            {
               Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
               return Ok(_playersService.GetPlayersByAccount(userInfo.Id));
            }
            catch (System.Exception e)
            {
              return BadRequest(e.Message);
            }
        }
    }


}