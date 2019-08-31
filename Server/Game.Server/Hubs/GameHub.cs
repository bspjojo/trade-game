using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Game.Server.Hubs
{
    public class GameHub : Hub<IGameHub>
    {
        private readonly ILogger<GameHub> _logger;

        public GameHub(ILogger<GameHub> logger)
        {
            _logger = logger;
        }

        public Task JoinGame(string gameId)
        {
            var connectionId = Context.ConnectionId;

            _logger.LogInformation($"Connection {connectionId} is joining game {gameId}");

            return Groups.AddToGroupAsync(connectionId, gameId);
        }

        public Task LeaveGame(string gameId)
        {
            var connectionId = Context.ConnectionId;

            _logger.LogInformation($"Connection {connectionId} is leaving game {gameId}");

            return Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId);
        }
    }
}