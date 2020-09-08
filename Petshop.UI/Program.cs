using Microsoft.Extensions.DependencyInjection;
using Petshop.Core.ApplicationServices;
using Petshop.Core.ApplicationServices.Impl;
using Petshop.Core.DomainServices;
using Petshop.Infrastructure.Data;

namespace Petshop.UI.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // create initial data
            FakeDB.InitData();

            // using ms dependency injection package
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IPetRepository, PetRepository>();
            serviceCollection.AddScoped<IPetService, PetService>();
            serviceCollection.AddScoped<IPrinter, Printer>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var printer = serviceProvider.GetRequiredService<IPrinter>();
            printer.ShowMenu();

            // To be removed:
            // By using serviceCollection, does this mean that the
            // repo dependency injection in the pet service also can be removed?
            //
            //IPetRepository petRepository = new PetRepository();
            //IPetService service = new PetService(petRepository);
        }
    }
}
