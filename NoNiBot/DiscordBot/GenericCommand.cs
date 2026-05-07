using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoNiDev.NoNiBot.DiscordBot
{
    public static class GenericCommand
    {
        private const string KEEPELAGO_LINK = "https://nicolasvalette.github.io/Archipelago-client/";

        public static async Task HandleGetKeepelagoLink(SocketSlashCommand command)
        {
            Embed msg = new EmbedBuilder()
                .WithTitle("Keepelago Link")
                .WithDescription($"Retrouve le client KEEPELAGO ici : \n\n{KEEPELAGO_LINK}")
                .WithFooter("NoNiBot à votre service")
                .WithColor(Color.Purple)
                .Build();
            await command.FollowupAsync(embed: msg);
        }
    }
}
