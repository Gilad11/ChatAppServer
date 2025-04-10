using ChatAppServer.Data;
using ChatAppServer.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    private static readonly ConcurrentDictionary<string, string> _userConnections = new();
    private static readonly ConcurrentDictionary<string, (string PlayerX, string PlayerO, string[] Board, string CurrentPlayer)> _games = new();

    public override async Task OnConnectedAsync()
    {
        var userId = Context.GetHttpContext()?.Request.Query["userId"];
        if (!string.IsNullOrEmpty(userId))
        {
            _userConnections[userId] = Context.ConnectionId;
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userEntry = _userConnections.FirstOrDefault(x => x.Value == Context.ConnectionId);
        if (!string.IsNullOrEmpty(userEntry.Key))
        {
            _userConnections.TryRemove(userEntry.Key, out _);
        }
        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessage(string senderId, string receiverId, string message)
    {
        if (_userConnections.TryGetValue(receiverId, out var connectionId))
        {
            await Clients.Client(connectionId).SendAsync("ReceiveMessage", senderId, message);
        }
    }

    public async Task SendGame(Game gameData)
    {
        if (gameData == null)
        {
            return;
        }

        if (_userConnections.TryGetValue(gameData.ReceiverId, out var receiverConnectionId))
        {
            await Clients.Client(receiverConnectionId).SendAsync("ReceiveGame", gameData);
        }
    }
}
