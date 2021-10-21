using System;
using System.Collections.Generic;
using cfbTransferPortal.Models;
using cfbTransferPortal.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace cfbTransferPortal.Services
{
    public class PlayersService
    {
      private readonly PlayersRepository _playersRepository;

    public PlayersService(PlayersRepository playersRepository)
    {
      _playersRepository = playersRepository;
    }

    public List<Player> GetAll()
    {
      return _playersRepository.GetAll();
    }

    public Player GetById(int playerId)
    {
      Player foundPlayer = _playersRepository.GetById(playerId);
      if(foundPlayer == null)
      {
        throw new Exception("Done didn't find no player by that id");
      }
      return foundPlayer;
    }

    public List<Player> GetPlayersByTeam(int teamId)
    {
      return _playersRepository.GetPlayersByTeam(teamId);
    }

    internal ActionResult<Player> Update(Player updatedPlayer, string userId)
    {
      Player foundPlayer = GetById(updatedPlayer.Id);
      if(foundPlayer.TeamId != null)
      {
        throw new Exception("get your hands off my player - you can't have");
      }
      foundPlayer.TeamId = updatedPlayer.TeamId;
      return _playersRepository.Update(foundPlayer);
    }

    internal List<Player> GetPlayersByAccount(string id)
    {
      return _playersRepository.GetPlayersByAccount(id);
    }
  }
}