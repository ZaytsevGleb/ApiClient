using GenerationClient.Core.Entities;
using GenerationClient.Core.Interfaces;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace GenerationClient.Core.Stratagies
{
    public class TransferProductsStrategy : IStrategy
    {
        private readonly IUserInterfaceService ui;
        private readonly IApplicationDbContext db;
        private readonly IProductsApiClient productsApiClient;

        public int OperationId => 2;

        public TransferProductsStrategy(
            IUserInterfaceService ui,
            IApplicationDbContext db,
            IProductsApiClient productsApiClient)
        {
            this.ui = ui;
            this.db = db;
            this.productsApiClient = productsApiClient;
        }

        public async Task RunAsync()
        {
            var response = await productsApiClient.FindAsync(null);
            var productDtos = response.Result;
            var products = productDtos
                .Select(product => new Product 
                {
                    Id = Guid.NewGuid(),
                    Name = product.Name,
                    CreatedDate = DateTime.Now
                });

            db.Products.RemoveRange(db.Products);
            db.Products.AddRange(products);
            await db.SaveChangesAsync();

            ui.PrintLine($"{productDtos.Count} products was added to database");
            ui.PrintLine("");
            ui.PrintLine("Press key to continue");
            ui.ReadLine();
        }
    }
}
