using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using cfbTransferPortal.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace cfbTransferPortal.Repositories
{
    public class PlayersRepository
    {
      private readonly IDbConnection _db;

    public PlayersRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Player> GetAll()
    {
     string sql = "SELECT * FROM players";
     return _db.Query<Player>(sql).ToList();
    }

    internal Player GetById(int playerId)
    {
      // TODO WHEN a player DOES have a team, we should populate that team AND the teams owner

      // using just a * here rather than p.*, t.*, etc.
      string sql = @"
      SELECT
      p.*,
      t.*,
      a.*
      FROM players p
      LEFT JOIN teams t on t.id = p.teamId
      LEFT JOIN accounts a on a.id = t.creatorId
      WHERE p.id = @playerId;
      ";
      return _db.Query<Player, Team, Account, Player>(sql, (p, t, a) => 
      {
        if(t != null)
        {
        t.Owner = a;
        p.Team = t;
        }
        return p;
      }, new{playerId}).FirstOrDefault();
    }

    internal List<Player> GetPlayersByTeam(int teamId)
    {
      string sql = @"
      SELECT * FROM players p WHERE p.teamId = @teamId
      ";
      return _db.Query<Player>(sql, new{teamId}).ToList();
    }

    internal List<Player> GetPlayersByAccount(string userId)
    {
      string sql = @"
      SELECT
      p.*,
      t.*
      FROM teams t
      JOIN players p on p.teamId = t.id
      WHERE t.creatorId = @userId;
      ";
      return _db.Query<Player, Team, Player>(sql, (p, t) =>
      {
        p.Team = t;
        return p;
      }, new{userId} ).ToList();
    }

    internal ActionResult<Player> Update(Player foundPlayer)
    {
      string sql = @"
      UPDATE players
      SET
      teamId = @TeamId
      WHERE id = @Id;
      ";
      var rowsAffected = _db.Execute(sql, foundPlayer);
      if(rowsAffected == 0)
      {
        throw new Exception("Update failed...");
      }
        return foundPlayer;
    }

    // Repository layer stuff goes here
  }
}