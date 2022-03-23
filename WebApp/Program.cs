using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WebApp
{
    /// <summary>
    /// Класс Program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Точка входа программы.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Метод IHostBuilder.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
