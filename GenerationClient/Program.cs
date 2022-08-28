using GenerationClient.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace GenerationClient
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var services = new ServiceCollection()
                .RegisterDependencies()
                .BuildServiceProvider();

            var application = services.GetRequiredService<Application>();
            await application.RunAsync();
        }
    }
}