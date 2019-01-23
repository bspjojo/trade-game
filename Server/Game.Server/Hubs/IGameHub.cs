using System.Threading.Tasks;
using Game.Server.Services.Models;

namespace Game.Server.Hubs
{
    public interface IGameHub
    {
        Task ScoresUpdated(ScoreUpdatedBroadcastModel broadcastModel);
    }
}