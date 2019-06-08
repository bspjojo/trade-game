BEGIN TRAN

-- Scenario

CREATE TABLE dbo.Scenarios
(
    ID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID()),
    [Name] VARCHAR(100),
    [Author] VARCHAR(200),
    [DateCreated] DATE,
    Duration int
)

CREATE TABLE dbo.Scenario_Countries
(
    ID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID()),
    ScenarioID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.Scenarios(ID),
    [Name] VARCHAR(100),
    TargetScore INT,
    -- produce
    Produce_Grain INT,
    Produce_Meat INT,
    Produce_Oil INT,
    Produce_Cocoa INT,
    Produce_Cotton INT,
    -- targets
    Target_Grain INT,
    Target_Meat INT,
    Target_Energy INT,
    Target_Chocolate INT,
    Target_Textiles INT
)

-- Game

CREATE TABLE dbo.Games
(
    ID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID()),
    ScenarioID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES dbo.Scenarios(ID),
    [Name] VARCHAR(100),
    [CurrentYear] INT,
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

ROLLBACk TRAN