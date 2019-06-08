-- SEED

insert INTO dbo.Scenarios
    (Name, DateCreated, Duration, Author)
values
    ('SeedScenario1', '2008-11-11', 5, 'Seeded')

-- select *
-- from dbo.Scenarios
-- order by Name

DECLARE @ScenarioId UNIQUEIDENTIFIER = (SELECT TOP 1
    ID
from dbo.Scenarios)

INSERT INTO dbo.Scenario_Countries
    (ScenarioID, Name, TargetScore, Produce_Grain, Produce_Meat, Produce_Oil, Produce_Cocoa, Produce_Cotton, Target_Grain, Target_Meat, Target_Energy, Target_Chocolate, Target_Textiles)
values
    (@ScenarioId, 'SeedCountry1', 40, 3, 4, 5, 6, 7, 3, 4, 5, 6, 7)

select *
from dbo.Scenario_Countries

DECLARE @ScenarioCountryId UNIQUEIDENTIFIER = (SELECT TOP 1
    ID
from dbo.Scenario_Countries)
-- select *
-- from dbo.BaseLine_Scenario_Country_Produce

insert into dbo.Games
    (ScenarioID, Name, DateStarted, Active, CurrentYear)
values
    (@ScenarioId, 'Seed game 1', '2018-10-11', 1, 1)

-- select *
-- from dbo.Games

declare @GameId UNIQUEIDENTIFIER = (SELECT top 1
    ID
from dbo.Games)

insert into dbo.Game_Countries
    ( GameID, ScenarioCountryID)
VALUES
    (@GameId, @ScenarioCountryId)

declare @GameCountryID UNIQUEIDENTIFIER =( SELECT top 1
    Id
from dbo.Game_Countries)

-- select *
-- from dbo.Game_Countries

INSERT INTO Game_Country_Year_Targets
    (GameCountryID, Year, Grain, Meat, Energy, Chocolate, Textiles)
VALUES
    ( @GameCountryID, 1, 3, 4, 5, 6, 7)