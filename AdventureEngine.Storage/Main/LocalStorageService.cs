namespace AdventureEngine.Storage.Main;

public class LocalStorageService : IStorageProvider
{
    private readonly string _dataPath;
    private readonly JsonSerializerOptions _jsonOptions;

    public async Task SaveSessionAsync(GameSession session)
    {
        var filePath = Path.Combine(_dataPath, "saves", $"{session.Id}.json");
        var json = JsonSerializer.Serialize(session, _jsonOptions);
        await File.WriteAllTextAsync(filePath, json);
    }

    public async Task<GameSession> LoadSessionAsync(string sessionId)
    {
        var filePath = Path.Combine(_dataPath, "saves", $"{sessionId}.json");
        if (!File.Exists(filePath)) return null;
        
        var json = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<GameSession>(json, _jsonOptions);
    }
}