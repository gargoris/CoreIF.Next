namespace EngineInConsole;

class Program
{
    static async Task Main(string[] args)
    {
        var engine = ConfigureEngine();
        var runner = new ConsoleGameRunner(engine);
        
        Console.WriteLine("=== Motor de Aventuras Conversacionales ===");
        await runner.StartAsync();
    }

    static IGameEngine ConfigureEngine()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IStorageProvider, LocalStorageService>();
        services.AddSingleton<IStoryRepository, FileSystemStoryRepository>();
        services.AddSingleton<IPluginManager, PluginManager>();
        services.AddTransient<StandaloneGameEngine>();
        
        var provider = services.BuildServiceProvider();
        return provider.GetService<StandaloneGameEngine>();
    }
}