using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;

namespace MetaSmite {
    class Program {
        private static Obj_AI_Minion Minion;
        public static Obj_AI_Hero Player => ObjectManager.Player;
        public static Spell SmiteSpell;

        private static void Main(string[] args) {
            try {
                var efslot = Player.Spellbook.Spells.FirstOrDefault(x => x.Name.ToLower().Contains("smite"));
                if (efslot != null) {
                    Game.PrintChat("<font color='#F5D76E'>Loaded MetaSmite by kyps.</font>");
                    SmiteSpell = new Spell(efslot.Slot, 600, TargetSelector.DamageType.True);
                }
            } catch (Exception e) {
                Console.WriteLine("Error: '{0}'", e);
            }
            Game.OnUpdate += Game_OnTick;
        }

        private static void Game_OnTick(EventArgs args) {
            try {
                if (Player.IsDead) {
                    return;
                }
                Minion = (Obj_AI_Minion) MinionManager.GetMinions(Player.ServerPosition, 600f, MinionTypes.All, MinionTeam.Neutral, MinionOrderTypes.MaxHealth).FirstOrDefault();
                if (Minion == null) {
                    return;
                }
                if (Minion.MaxHealth < 4900) {
                    return;
                }
                if (SmiteSpell.IsReady()) {
                    int[] damages = { 20 * Player.Level + 370, 30 * Player.Level + 330, 40 * Player.Level + 240, 50 * Player.Level + 100 };
                    if (damages.Max() >= Minion.Health && SmiteSpell.CanCast(Minion)) {
                        Player.Spellbook.CastSpell(SmiteSpell.Slot, Minion);
                    }
                }
            } catch (Exception e) {
                Console.WriteLine("Error: '{0}'", e);
            }
        }
    }
}