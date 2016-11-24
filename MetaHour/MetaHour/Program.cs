using System;
using LeagueSharp;
using System.Drawing;

namespace MetaHour {
    class Program {

        private static void Main(string[] args) {
            Drawing.OnDraw += Game_OnDraw;
            Game.PrintChat("<font color='#F5D76E'>Loaded MetaHour by kyps.</font>");
        }

        private static void Game_OnDraw(EventArgs args) {
            Drawing.DrawText(Drawing.Width - (Drawing.Width / 3), 10, Color.White, "" + DateTime.Now);
        }

    }
}