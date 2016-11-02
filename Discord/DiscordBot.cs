using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using IA;
using System.Text.RegularExpressions;
using System.Net;

namespace Discord
{
    class DiscordBot
    {
        DiscordClient discord;
        string receiveMsg;

        string linkURL;
        string html;

        string ranText;
        Random rand = new Random();
        Match match;
        string[] helloMsg =
            "ดีจ้า|สวัสดีจ้า|สวัสดีครับ|ดีครัช|โจ้วๆ สวัสดี|โย่วๆ สวัสดี".Split('|');

        public DiscordBot(string BotToken)
        {
            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });
            Connect(BotToken);
            discord.UsingCommands(x =>
            {
                x.PrefixChar = '!';
                x.AllowMentionPrefix = true;
            });

            var commands = discord.GetService<CommandService>();
            //////////////////////////
            commands.CreateCommand("help")
            .Do(async (e) =>
            {
                await e.Channel.SendMessage("ส วั ส ดี จ้ า ! " + e.User.Mention + " :)");
            });
            //////////////////////////
            discord.MessageReceived += async (s, e) =>
            {
                receiveMsg = e.Message.Text;
                if (!e.Message.IsAuthor)
                {
                    //if (receiveMsg == "ดีครับ")
                    //{
                    //    await e.Channel.SendMessage("ดีจ้า");
                    //}
                    //////////////////////////
                    match = Regex.Match(receiveMsg, @"^(ดีคั)|^(ดีครั)|^(สวัสดี)|^(หวัดดี)|^(ดีฮะ)|^(ดีฮั)|^(ดีจ้)|^(ดีจ๊)|^(ดีจ่)", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        ranText = helloMsg[rand.Next(helloMsg.Length)];
                        await e.Channel.SendMessage(e.User.Mention + " " + ranText + " o.o/");
                    }
                    ////////////////////////// TODO
                    match = Regex.Match(receiveMsg, @"^(http:\/\/)(.+)|^(https:\/\/)(.+)|^(www.)(.+).(co.th|in.th|or.th|go.th|tv|me|com|com.sg|org)", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        linkURL = match.Groups[0].Value;
                        html = await new WebClient().DownloadStringTaskAsync(linkURL);
                        match = Regex.Match(html, @"<...>(.+)<\/...>", RegexOptions.IgnoreCase);
                        if (match.Success)
                        {
                            await e.Channel.SendMessage(match.Groups[1].Value);
                        }
                    }
                }
            };
            //////////////////////////
        }
        ///////////////////////////////////////////////

        public void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        public void Connect(string Token)
        {
            discord.Connect(Token, TokenType.Bot);
        }
        public void Disconnect()
        {
            discord.Disconnect();
        }
    }
}
