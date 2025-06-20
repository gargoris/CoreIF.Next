namespace AdventureEngine.Storage.Main;

public class FileSystemStoryRepository : IStoryRepository
{
    private readonly string _storiesPath;

    public async Task<Story> LoadStoryAsync(string storyPath)
    {
        var fullPath = Path.Combine(_storiesPath, storyPath);
        var extension = Path.GetExtension(fullPath).ToLower();

        return extension switch
        {
            ".json" => await LoadJsonStoryAsync(fullPath),
            ".yaml" => await LoadYamlStoryAsync(fullPath),
            ".md" => await LoadMarkdownStoryAsync(fullPath),
            _ => throw new NotSupportedException($"Story format {extension} not supported")
        };
    }

    public async Task<List<StoryInfo>> GetAvailableStoriesAsync()
    {
        var stories = new List<StoryInfo>();
        var files = Directory.GetFiles(_storiesPath, "*.*", SearchOption.AllDirectories)
            .Where(f => IsStoryFile(f));

        foreach (var file in files)
        {
            var info = await ExtractStoryInfoAsync(file);
            stories.Add(info);
        }

        return stories;
    }
}