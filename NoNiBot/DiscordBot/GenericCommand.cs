using Discord;
using Discord.WebSocket;

namespace NoNiDev.NoNiBot.DiscordBot
{
    public static class GenericCommand
    {
        private static Random _rand = new Random();
        private const string KEEPELAGO_LINK = "https://nicolasvalette.github.io/Archipelago-client/";

        public static async Task HandleGetKeepelagoLink(SocketSlashCommand command)
        {
            try
            {
                Embed msg = new EmbedBuilder()
                    .WithTitle("Keepelago Link")
                    .WithDescription($"Retrouve le client KEEPELAGO ici : \n\n{KEEPELAGO_LINK}")
                    .WithFooter("NoNiBot à votre service")
                    .WithColor(Color.Purple)
                    .Build();
                await command.FollowupAsync(embed: msg);
            }
            catch (Exception ex)
            {
                await command.FollowupAsync($"❌ Une erreur est survenue : {ex.Message}");
            }
        }

        public static async Task HandleRoll6(SocketSlashCommand command)
        {
            try
            {
                int roll = _rand.Next(1, 7);
                Embed msg = new EmbedBuilder()
                    .WithTitle("Lancement des dès")
                    .WithDescription($"Résultats du lancé \n [{roll}]")
                    .WithFooter("NoNiBot à votre service")
                    .WithColor(Color.Blue)
                    .Build();
                await command.FollowupAsync(embed: msg);
            }
            catch (Exception ex)
            {
                await command.FollowupAsync($"❌ Une erreur est survenue : {ex.Message}");
            }
        }

        public static async Task HandleRoll(SocketSlashCommand command)
        {
            try
            {
                
                var valueFace = command.Data.Options.FirstOrDefault(x => x.Name == "face")?.Value;
                int face = Convert.ToInt32(valueFace);
                if (face > 1000 || face <= 0)
                {
                    await command.FollowupAsync("❌ Le nombre de faces doit être compris entre 1 et 1000.");
                    return;
                }

                var nbDiceValue = command.Data.Options.FirstOrDefault(x => x.Name == "dices")?.Value;
                int nbDice = Convert.ToInt32(nbDiceValue);
                if (nbDice > 100 || nbDice <= 0)
                {
                    await command.FollowupAsync("❌ Le nombre de dés doit être compris entre 1 et 100.");
                    return;
                }
                List<int> rolls = new List<int>();
                for (int i = 0; i < nbDice; i++)
                {
                    rolls.Add(_rand.Next(1, face + 1));
                }

                Embed msg = new EmbedBuilder()
                    .WithTitle("Lancement des dès")
                    .WithDescription($"Résultats du {nbDice}D{face} \n [{string.Join(", ", rolls)}]")
                    .WithFooter("NoNiBot à votre service")
                    .WithColor(Color.DarkBlue)
                    .Build();
                await command.FollowupAsync(embed: msg);
            }
            catch (Exception ex)
            {
                await command.FollowupAsync($"❌ Une erreur est survenue : {ex.Message}");
            }
        }
    }
}
