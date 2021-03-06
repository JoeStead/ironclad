﻿// Copyright (c) Lykke Corp.
// See the LICENSE file in the project root for more information.

namespace Ironclad.Console.Commands
{
    using System.Threading.Tasks;
    using McMaster.Extensions.CommandLineUtils;

    internal class EnableClientCommand : ICommand
    {
        private string clientId;

        private EnableClientCommand()
        {
        }

        public static void Configure(CommandLineApplication app, CommandLineOptions options)
        {
            // description
            app.Description = "Enable a client";
            app.HelpOption();

            // arguments
            var argumentClientId = app.Argument("id", "The client identifier", false);

            // action (for this command)
            app.OnExecute(
                () =>
                {
                    if (string.IsNullOrEmpty(argumentClientId.Value))
                    {
                        app.ShowHelp();
                        return;
                    }

                    options.Command = new EnableClientCommand
                    {
                        clientId = argumentClientId.Value
                    };
                });
        }

        public async Task ExecuteAsync(CommandContext context)
        {
            var client = new Ironclad.Client.Client
            {
                Id = this.clientId,
                Enabled = true
            };

            await context.ClientsClient.ModifyClientAsync(client).ConfigureAwait(false);
        }
    }
}