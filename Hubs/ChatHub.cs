using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

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

    public async Task JoinGame(string gameId, string userId)
    {
        if (!_games.ContainsKey(gameId))
        {
            _games[gameId] = (userId, "", new string[9], "X");
        }
        else if (_games[gameId].PlayerO == "")
        {
            _games[gameId] = (_games[gameId].PlayerX, userId, new string[9], "X");
        }
        await Clients.Client(Context.ConnectionId).SendAsync("AssignPlayer", _games[gameId].PlayerX == userId ? "X" : "O");
    }

    public async Task MakeMove(string gameId, string userId, int index)
    {
        if (!_games.ContainsKey(gameId) || index < 0 || index > 8) return;
        var game = _games[gameId];

        if (game.Board[index] != null || game.CurrentPlayer != (game.PlayerX == userId ? "X" : "O")) return;

        game.Board[index] = game.CurrentPlayer;
        game.CurrentPlayer = game.CurrentPlayer == "X" ? "O" : "X";
        _games[gameId] = game;

        await Clients.Group(gameId).SendAsync("UpdateGame", new { Board = game.Board, CurrentPlayer = game.CurrentPlayer });
    }

    public async Task ResetGame(string gameId)
    {
        if (!_games.ContainsKey(gameId)) return;
        var game = _games[gameId];
        _games[gameId] = (game.PlayerX, game.PlayerO, new string[9], "X");
        await Clients.Group(gameId).SendAsync("UpdateGame", new { Board = new string[9], CurrentPlayer = "X" });
    }
}
