SELECT * FROM players;

SELECT * FROM teams;

-- CREATE TABLE IF NOT EXISTS players(
--   id int NOT NULL AUTO_INCREMENT PRIMARY KEY, 
--   teamId int,
--   name VARCHAR(255) NOT NULL,
--   picture VARCHAR(255) NOT NULL,
--   position VARCHAR(10) NOT NULL,
--   class VARCHAR(255) NOT NULL,
--   height int NOT NULL,
--   weight int NOT NULL,

--   FOREIGN KEY(teamId) REFERENCES teams(id)
-- ) default charset utf8 COMMENT '';
SELECT * FROM accounts;

CREATE TABLE IF NOT EXISTS teams(
  id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(255) NOT NULL,
  conference VARCHAR(255) NOT NULL,
  division VARCHAR(255) NOT NULL,
  picture VARCHAR(255) NOT NULL,

  FOREIGN KEY(creatorId) REFERENCES accounts(id) ON DELETE CASCADE

) default charset utf8 COMMENT '';



SELECT
      p.*,
      t.*
      FROM players p
      LEFT JOIN teams t on t.id = p.teamId
      WHERE p.id = 2;
