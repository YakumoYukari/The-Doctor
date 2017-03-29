using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace TheDoctor.Commands
{
    public class RecordCommand : IBotCommand
    {
        public string Command { get; } = "record";

        public string Description { get; } = "Records the last -n <num> messages.";

        private const string CommandFormat = @"-([mt]) (\d+)( .*)?$";

        public Func<CommandEventArgs, IBot, Task> Function { get; } = async (Event, Bot) =>
        {
            string Args = Event.GetArg(CommandBuilder.Args).Trim();
            if (!Regex.IsMatch(Args, CommandFormat))
                return;

            var Match = Regex.Match(Args, CommandFormat);

            await LogMessages(Event, Match);
        };

        private static async Task LogMessages(CommandEventArgs Args, Match ArgsMatch)
        {
            var Channel = Args.Server.FindChannels("linkrepo").FirstOrDefault();
            if (Channel == null) return;

            int Number = int.Parse(ArgsMatch.Groups[2].Value);
            string Reason = ArgsMatch.Groups[3].Value.Trim();

            IEnumerable<Message> ToLog = await FetchChatMessages(Args, ArgsMatch, Number);
            if (!ToLog.Any()) return;

            await LogDumpStart(Channel, Reason);
            await DumpLogMessages(Channel, ToLog);
        }

        private static async Task<List<Message>> FetchChatMessages(CommandEventArgs Args, Match ArgsMatch, int Number)
        {
            IEnumerable<Message> ToLog = new List<Message>();

            if (ArgsMatch.Groups[1].Value == "m")
            {
                ToLog = (await Args.Channel.DownloadMessages(Number)).Reverse();
            }

            return ToLog.ToList();
        }

        private static async Task LogDumpStart(Channel Channel, string Reason)
        {
            await Channel.SendMessage("========== Starting Dump ==========");
            if (!string.IsNullOrWhiteSpace(Reason))
                await Channel.SendMessage($"Note: {Reason}");
        }

        private static async Task DumpLogMessages(Channel Channel, IEnumerable<Message> ToLog)
        {
            var Output = new StringBuilder();
            foreach (var M in ToLog)
            {
                string ToAppend = $"{M.User.Name}: {M.Text}";

                if (Output.Length + ToAppend.Length > 2000)
                {
                    await Channel.SendMessage(Output.ToString());
                    await Task.Delay(1000);
                    Output.Clear();
                }
                Output.AppendLine(ToAppend);
            }

            if (Output.Length > 0)
                await Channel.SendMessage(Output.ToString());
        }
    }
}
