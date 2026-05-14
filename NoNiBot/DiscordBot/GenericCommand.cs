using Discord;
using Discord.WebSocket;
using System.Text;

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
                    .WithTitle("Lancement du dès")
                    .WithDescription($":game_die: => [{roll}]")
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
                int[] valuesSum = new int[face];
                for (int i = 0; i < nbDice; i++)
                {
                    int value = _rand.Next(1, face + 1);
                    valuesSum[value - 1]++;
                    rolls.Add(value);
                }
                StringBuilder strb = new();
                strb.AppendLine($"Résultats du {nbDice}D{face}");
                strb.AppendLine();
                strb.Append($"[{ string.Join(", ", rolls)}]");
                strb.AppendLine();
                strb.AppendLine();
                strb.AppendLine("Tableau de résultats par face : ");
                for (int i = 0; i < valuesSum.Length; i++)
                {
                    strb.AppendLine($"Face [{i+1}] => {valuesSum[i]}");
                }
                strb.AppendLine();
                strb.AppendLine($"Somme de tout les dés : {rolls.Sum()}");
                Embed msg = new EmbedBuilder()
                    .WithTitle("Lancement des dès")
                    .WithDescription(strb.ToString())
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
