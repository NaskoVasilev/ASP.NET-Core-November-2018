using AngleSharp.Parser.Html;
using FunApp.Data;
using FunApp.Data.Common;
using FunApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace SandBox
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($"{typeof(Program).Namespace} ({string.Join(" ", args)}) starts working...");

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider(true);

            using (var serviceScope = serviceProvider.CreateScope())
            {
                serviceProvider = serviceScope.ServiceProvider;
                SandboxCode(serviceProvider);
            }
        }

        private static void SandboxCode(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<FunAppDbContext>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var parser = new HtmlParser();
            var webClient = new WebClient { Encoding = Encoding.GetEncoding("windows-1251") };
            List<Joke> jokes = new List<Joke>();

            for (int i = 3020; i < 5000; i++)
            {
                var url = "http://fun.dir.bg/vic_open.php?id=" + i;
                string html = null;

                for (int j = 0; j < 5; j++)
                {
                    try
                    {
                        html = webClient.DownloadString(url);
                        break;
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(3000);
                    }
                }

                if (string.IsNullOrWhiteSpace(html))
                {
                    continue;
                }

                var document = parser.Parse(html);
                var jokeContent = document.QuerySelector("#newsbody")?.TextContent?.Trim();
                var categoryName = document.QuerySelector(".tag-links-left a")?.TextContent?.Trim();

                if (!string.IsNullOrWhiteSpace(jokeContent) &&
                  !string.IsNullOrWhiteSpace(categoryName))
                {
                    var category = context.Categories.FirstOrDefault(x => x.Name == categoryName);
                    if (category == null)
                    {
                        category = new Category
                        {
                            Name = categoryName,
                        };
                    }

                    var joke = new Joke()
                    {
                        Category = category,
                        Content = jokeContent,
                    };

                    jokes.Add(joke);

                    if (jokes.Count >= 50)
                    {
                        context.AddRange(jokes);
                        context.SaveChanges();
                        jokes.Clear();
                    }
                }

                Console.WriteLine($"{i} => {categoryName}");
            }
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string path = Directory.GetParent(currentDirectory).Parent.Parent.FullName;
            var configuration = new ConfigurationBuilder().SetBasePath(path)
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            services.AddDbContext<FunAppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
        }
    }
}
