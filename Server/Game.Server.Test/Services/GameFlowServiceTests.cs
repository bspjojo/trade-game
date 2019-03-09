// using System.Collections.Concurrent;
// using Game.Server.Services;
// using Game.Server.Services.Models;
// using Moq;
// using Xunit;

// namespace Game.Server.Test.Services
// {
//     public class GameFlowServiceTests
//     {
//         private Mock<IGameDataService> _mockIGameDataService;
//         private Mock<IGameHubService> _mockIGameHubService;
//         private Mock<IGameScoreService> _mockIGameScoreService;

//         private GameFlowService _gameFlowService;
//         private GameCountry _gameCountry;

//         public GameFlowServiceTests()
//         {
//             _gameCountry = new GameCountry
//             {
//                 Years = new ConcurrentDictionary<int, CountryYear>()
//             };

//             _gameCountry.Years.TryAdd(0, new CountryYear
//             {
//                 Excess = new ConsumptionResources(),
//                 Scores = new ConsumptionResources(),
//                 Targets = new ConsumptionResources()
//             });

//             _mockIGameDataService = new Mock<IGameDataService>();
//             _mockIGameDataService.Setup(m => m.GetCountryById("game", "country")).ReturnsAsync(() => _gameCountry);

//             _mockIGameHubService = new Mock<IGameHubService>();
//             _mockIGameScoreService = new Mock<IGameScoreService>();

//             _gameFlowService = new GameFlowService(_mockIGameDataService.Object, _mockIGameHubService.Object, _mockIGameScoreService.Object);
//         }

//         [Fact]
//         public async void ExecuteUpdateScoreFlow_ShouldGetTheCountryFromTheGameDataService()
//         {
//             var resources = new ConsumptionResources
//             {
//                 Chocolate = 3,
//                 Meat = 3,
//                 Textiles = 3,
//                 Grain = 3,
//                 Energy = 3
//             };

//             await _gameFlowService.ExecuteUpdateScoreFlow("game", "country", 0, resources);

//             _mockIGameDataService.Verify(m => m.GetCountryById("game", "country"));
//         }

//         [Fact]
//         public async void ExecuteUpdateScoreFlow_ShouldAddAYearToTheCountry()
//         {
//             var resources = new ConsumptionResources
//             {
//                 Chocolate = 3,
//                 Meat = 3,
//                 Textiles = 3,
//                 Grain = 3,
//                 Energy = 3
//             };

//             await _gameFlowService.ExecuteUpdateScoreFlow("game", "country", 0, resources);

//             _gameCountry.Years.TryGetValue(1, out var c);

//             Assert.NotNull(c);
//         }

//         [Fact]
//         public async void ExecuteUpdateScoreFlow_ShouldCallGameScoreServiceToCalculateTheYearValues()
//         {
//             var resources = new ConsumptionResources
//             {
//                 Chocolate = 3,
//                 Meat = 3,
//                 Textiles = 3,
//                 Grain = 3,
//                 Energy = 3
//             };

//             await _gameFlowService.ExecuteUpdateScoreFlow("game", "country", 0, resources);

//             _mockIGameScoreService.Verify(m => m.CalculateYearValues(0, _gameCountry, resources));
//         }

//         [Fact]
//         public async void ExecuteUpdateScoreFlow_ShouldCallTheGameHubServiceToSignalAnUpdateToTheScores()
//         {
//             var resources = new ConsumptionResources
//             {
//                 Chocolate = 3,
//                 Meat = 3,
//                 Textiles = 3,
//                 Grain = 3,
//                 Energy = 3
//             };

//             await _gameFlowService.ExecuteUpdateScoreFlow("game", "country", 0, resources);

//             var scores = _gameCountry.Years[0].Scores;

//             _mockIGameHubService.Verify(m => m.ScoresUpdated("game", "country", 0, scores));
//         }

//         [Fact]
//         public async void ExecuteUpdateScoreFlow_ShouldReturnAnObjectWithNextYearsTargetsAndTheExcess()
//         {
//             var resources = new ConsumptionResources
//             {
//                 Chocolate = 3,
//                 Meat = 3,
//                 Textiles = 3,
//                 Grain = 3,
//                 Energy = 3
//             };

//             _mockIGameScoreService.Setup(m => m.CalculateYearValues(It.IsAny<int>(), It.IsAny<GameCountry>(), It.IsAny<ConsumptionResources>())).Callback(() =>
//             {
//                 _gameCountry.Years[1].Targets = new ConsumptionResources();
//             });

//             var result = await _gameFlowService.ExecuteUpdateScoreFlow("game", "country", 0, resources);

//             Assert.NotNull(result.Excess);
//             Assert.Equal(_gameCountry.Years[0].Excess, result.Excess);

//             Assert.NotNull(result.NextYearTarget);
//             Assert.Equal(_gameCountry.Years[1].Targets, result.NextYearTarget);
//         }
//     }
// }