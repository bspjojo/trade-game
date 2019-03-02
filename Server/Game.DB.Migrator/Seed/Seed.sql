

-- SEED

insert INTO dbo.Scenarios
    (Name,DateCreated,Duration)
values
    ('SeedScenario1', '2008-11-11', 5)

-- select *
-- from dbo.Scenarios
-- order by Name

DECLARE @ScenarioId UNIQUEIDENTIFIER = (SELECT TOP 1
    ID
from dbo.Scenarios)

INSERT INTO dbo.Scenario_Countries
    (ScenarioID, Name)
values
    (@ScenarioId, 'SeedCountry1')

-- select *
-- from dbo.Scenario_Countries
DECLARE @ScenarioCountryId UNIQUEIDENTIFIER = (SELECT TOP 1
    ID
from dbo.Scenario_Countries)

INSERT INTO dbo.BaseLine_Scenario_Country_Targets
    (ScenarioCountryID, Grain, Meat, Energy, Chocolate, Textiles)
values(@ScenarioCountryId, 3, 4, 5, 6, 7)

-- select *
-- from dbo.BaseLine_Scenario_Country_Targets

INSERT INTO dbo.BaseLine_Scenario_Country_Produce
    (ScenarioCountryID, Grain, Meat, Oil, Cocoa, Cotton)
values(@ScenarioCountryId, 3, 4, 5, 6, 7)

-- select *
-- from dbo.BaseLine_Scenario_Country_Produce

insert into dbo.Games
    (ScenarioID, Name, DateStarted, Active)
values
    (@ScenarioId, 'Seed game 1', '2018-10-11', 1)

-- select *
-- from dbo.Games

declare @GameId UNIQUEIDENTIFIER = (SELECT top 1
    ID
from dbo.Games)

insert into dbo.Game_Countries
    ( GameID, ScenarioCountryID)
VALUES
    (@GameId, @ScenarioCountryId)

select *
from dbo.Game_Countries