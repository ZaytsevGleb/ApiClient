using GenerationClient.Core.Exceptions;
using GenerationClient.Core.Interfaces;
using GenerationClient.Core.Stratagies;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenerationClient.Core
{
    public class Application
    {
        private readonly IUserInterfaceService ui;
        private readonly IEnumerable<IStrategy> strategies;
        private readonly IConfiguration configuration;

        public Application(
            IUserInterfaceService ui,
            IEnumerable<IStrategy> strategies,
            IConfiguration configuration)
        {
            this.ui = ui;
            this.strategies = strategies;
            this.configuration = configuration;
        }

        public async Task RunAsync()
        {
            try
            {
                do
                {
                    ui.Clear();

                    ui.PrintLine("Please select operation:");
                    ui.PrintLine("1: Show existings");
                    ui.PrintLine("2: Full database");
                    ui.PrintLine("3: Exit");

                    var selectedOperation = ui.ReadNumber();
                    var strategy = strategies.FirstOrDefault(x => x.OperationId == selectedOperation);
                    if (strategy == null)
                    {
                        if (selectedOperation == 3)
                            throw new CancelAppException();
                        else
                            throw new InvalidOperationException("Wrong choice");
                    }

                    ui.Clear();
                    await strategy.RunAsync();

                } while (true);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        private async Task HandleExceptionAsync(Exception exception)
        {
            if (exception is CancelAppException)
            {
                ui.PrintLine("Application cancel");
                return;
            }

            if (Convert.ToBoolean(configuration["DeveloperMode"]))
            {
                ui.PrintLine(exception.ToString());
                ui.PrintLine("\nPress button to continue");
                ui.ReadLine();
            }

            ui.Clear();
            ui.PrintLine("Something goes wrong. Do you want try again: yes/no");
            var confirmation = ui.ReadLine();

            switch (confirmation)
            {
                case "yes":
                    ui.Clear();
                    await RunAsync();
                    break;

                case "no":
                    break;

                default:
                    throw new InvalidOperationException("Wrong choice");
            }
        }
    }
}
