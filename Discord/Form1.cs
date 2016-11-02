using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Discord
{
    public partial class Form1 : Form
    {
        static string ClientID = "...........";
        static string Token = "..............";
        DiscordBot bot = new DiscordBot(Token);
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAuth_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd";   //iexplore
            startInfo.Arguments = "/C start firefox https://discordapp.com/api/oauth2/authorize?client_id=" + ClientID + "&scope=bot&permissions=0";
            process.StartInfo = startInfo;
            process.Start();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            bot.Connect(Token);
        }
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            bot.Disconnect();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            bot.Disconnect();
        }
    }
}
