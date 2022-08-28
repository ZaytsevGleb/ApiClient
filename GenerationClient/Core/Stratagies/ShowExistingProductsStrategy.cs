using GenerationClient.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GenerationClient.Core.Stratagies
{
    internal class ShowExistingProductsStrategy : IStrategy
    {
        private readonly IUserInterfaceService ui;
        private readonly IApplicationDbContext db;

        public int OperationId => 1;

        public ShowExistingProductsStrategy(
            IUserInterfaceService ui,
            IApplicationDbContext db)
        {
            this.ui = ui;
            this.db = db;
        }

        public async Task RunAsync()
        {
            var products = await db.Products.ToListAsync();
            if (!products.Any())
            {
                ui.PrintLine("There is no items");
            }
            else
            {
                foreach (var product in products)
                {
                    ui.PrintLine($"Name: {product.Name}\t Date: {product.CreatedDate.ToString("dd:mm:yyyy")}");
                }
            }

            ui.PrintLine("");
            ui.PrintLine("Press key to continue");
            ui.ReadLine();
        }
    }
}
