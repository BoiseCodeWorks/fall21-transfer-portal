
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using cfbTransferPortal.Models;
using Dapper;

namespace cfbTransferPortal.Repositories
{
  public class TeamsRepository
  {
    private readonly IDbConnection _db;

    public TeamsRepository(IDbConnection db)
    {
      _db = db;
    }

    // GET ALL TEAMS
    // TODO maybe should populate dat team owner maybe
    internal List<Team> GetAll()
    {
      string sql = @"
      SELECT * FROM teams;
      ";
      return _db.Query<Team>(sql).ToList();
    }

    internal Team GetById(int teamId)
    {
      string sql = @"
      SELECT
      t.*,
      a.*
      FROM teams t
      JOIN accounts a on t.creatorId = a.id
      WHERE t.id = @teamId;
      ";
      return _db.Query<Team, Account, Team>(sql, (t, a) =>
      {
        t.Owner = a;
        return t;
      }, new{teamId}).FirstOrDefault();
    }

    internal Team Post(Team teamData)
    {
      string sql = @"
      INSERT INTO teams(creatorId, name, conference, division, picture)
      VALUES(@CreatorId, @Name, @Conference, @Division, @Picture);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, teamData);
      teamData.Id = id;
      return teamData;
    }

    internal List<Team> GetTeamsByAccount(string userId)
    {
      string sql = "SELECT * FROM teams t WHERE t.creatorId = @userId";
      return _db.Query<Team>(sql, new{userId}).ToList();
    }

    internal void RemoveTeam(int teamId)
    {
      // --------- THIS IS UPDATING THE PLAYERS TEAM ID BACK TO NULL BEFORE THE TEAM IS DELETED
      string updateSql = @"
      UPDATE players
      SET 
      teamId = null
      WHERE teamId = @teamId;
      ";
      var updatedRows = _db.Execute(updateSql, new {teamId});

      // --------------------------- THIS IS HARD DELETING THE TEAM ----------------------------
      string sql = "DELETE FROM teams WHERE id = @teamId LIMIT 1;";
      var affectedRows = _db.Execute(sql, new {teamId});
      if(affectedRows == 0)
      {
        throw new Exception("No dice.");
      }
    }
  }
}