using System;
using System.Collections.Generic;
using cfbTransferPortal.Models;
using cfbTransferPortal.Repositories;

namespace cfbTransferPortal.Services
{
    public class TeamsService
    {
      private readonly TeamsRepository _teamsRepository;

    public TeamsService(TeamsRepository teamsRepository)
    {
      _teamsRepository = teamsRepository;
    }

    public List<Team> GetAll()
    {
      return _teamsRepository.GetAll();
    }

    public Team GetById(int teamId)
    {
      Team foundTeam = _teamsRepository.GetById(teamId);
      if(foundTeam == null)
      {
        throw new Exception("Unable to find team");
      }
      return foundTeam;
    }

    public Team Post(Team teamData)
    {
      return _teamsRepository.Post(teamData);
    }

    public void RemoveTeam(int teamId, string userId)
    {
      Team foundTeam = GetById(teamId);
      if(foundTeam.CreatorId != userId)
      {
        throw new Exception("That aint your team");
      }
      _teamsRepository.RemoveTeam(teamId);
    }

    public List<Team> GetTeamsByAccount(string userId)
    {
      return _teamsRepository.GetTeamsByAccount(userId);
    }
  }
}