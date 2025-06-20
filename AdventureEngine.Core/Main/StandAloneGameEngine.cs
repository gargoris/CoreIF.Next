namespace AdventureEngine.Core.Main;

public class StandaloneGameEngine : IGameEngine
{
    private readonly IStorageProvider _storage;
    private readonly SessionManager _sessionManager;
    private readonly IPluginManager _pluginManager;
    private readonly ConversationProcessor _processor;

    public async Task<GameSession> StartNewGameAsync(string storyPath, string playerName)
    {
        var story = await LoadStoryAsync(storyPath);
        var session = _sessionManager.CreateNewSession(story, playerName);
        await _storage.SaveSessionAsync(session);
        return session;
    }

    public async Task<GameResponse> ProcessInputAsync(string sessionId, string input)
    {
        var session = await _storage.LoadSessionAsync(sessionId);
        var response = await _processor.ProcessAsync(session, input);
        await _storage.SaveSessionAsync(session);
        return response;
    }
}