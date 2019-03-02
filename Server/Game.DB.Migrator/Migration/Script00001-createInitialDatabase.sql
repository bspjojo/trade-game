BEGIN TRAN

-- Scenario

CREATE TABLE dbo.Scenarios
(
    ID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID()),
    [Name] VARCHAR(100),
    [DateCreated] DATE,
    Duration int
)

CREATE TABLE dbo.Scenario_Countries
(
    ID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID()),
    ScenarioID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.Scenarios(ID),
    [Name] VARCHAR(100),
    TargetScore INT
)

CREATE TABLE dbo.BaseLine_Scenario_Country_Produce
(
    ID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID()),
    ScenarioCountryID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.Scenario_Countries(ID),
    Grain INT,
    Meat INT,
    Oil INT,
    Cocoa INT,
    Cotton INT
)

CREATE TABLE dbo.BaseLine_Scenario_Country_Targets
(
    ID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID()),
    ScenarioCountryID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.Scenario_Countries(ID),
    Grain INT,
    Meat INT,
    Energy INT,
    Chocolate INT,
    Textiles INT
)

-- Game

CREATE TABLE dbo.Games
(
    ID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID()),
    ScenarioID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.Scenarios(ID),
    [Name] VARCHAR(100),
    [DateStarted] DATE,
    [Active] BIT
)

CREATE TABLE dbo.Game_Countries
(
    ID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID()),
    GameID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.Games(ID),
    ScenarioCountryID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.Scenario_Countries(ID),
)

CREATE TABLE dbo.Game_Country_Year_Recorded
(
    ID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID()),
    GameCountryID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.Game_Countries(ID),
    Year INT,
    Grain INT,
    Meat INT,
    Energy INT,
    Chocolate INT,
    Textiles INT
)

CREATE TABLE dbo.Game_Country_Year_Score
(
    ID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID()),
    GameCountryID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.Game_Countries(ID),
    Year INT,
    Grain INT,
    Meat INT,
    Energy INT,
    Chocolate INT,
    Textiles INT
)

CREATE TABLE dbo.Game_Country_Year_Excess
(
    ID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID()),
    GameCountryID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.Game_Countries(ID),
    Year INT,
    Grain INT,
    Meat INT,
    Energy INT,
    Chocolate INT,
    Textiles INT
)

CREATE TABLE dbo.Game_Country_Year_Targets
(
    ID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID()),
    GameCountryID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.Game_Countries(ID),
    Year INT,
    Grain INT,
    Meat INT,
    Energy INT,
    Chocolate INT,
    Textiles INT
)

CREATE TABLE dbo.Game_Country_Year_Growth_Certificates_Purchased
(
    ID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID()),
    GameCountryID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.Game_Countries(ID),
    Year INT,
    Grain INT,
    Meat INT,
    Oil INT,
    Cocoa INT,
    Cotton INT
)

COMMIT TRAN