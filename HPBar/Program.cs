//#define DEV

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Ensage;
using Ensage.Common;
using Ensage.Common.Menu;
using Ensage.Common.Extensions;
using SharpDX;
using SharpDX.Direct3D9;


namespace HPBar
{
    class Program
    {
        private static readonly Menu Menu = new Menu("HPBar Options", "HPBar", true);

        private static void Main()
        {
            Menu.AddItem(new MenuItem("HPBarShow", "HP Bar").SetValue(true));
            Menu.AddItem(new MenuItem("ManaBarShow", "Mana Bar").SetValue(true));
            Menu.AddToMainMenu();
#if (DEV)
            Console.WriteLine("HPBar - Main init");
#endif
            Drawing.OnDraw += Render_HUD;
        }

        private static void Render_HUD(EventArgs args)
        {
            for (uint i = 0; i < 10; i++)
            {
                var player = ObjectMgr.GetPlayerById(i);
                if (player == null) continue; 
                Hero h = player.Hero;
                if (h == null) continue;

                var Pos = HUDInfo.GetTopPanelPosition(h);
                var PosX = (float)HUDInfo.GetTopPanelSizeX(h);
                var PosY = (float)HUDInfo.GetTopPanelSizeY(h);

                var PosHealth = new Vector2(h.Health * PosX / h.MaximumHealth, 0);
                var PosMana = new Vector2(h.Mana * PosX / h.MaximumMana, 0);

                float BPos = PosY + 1;

                if (h.Health == 0) continue;
                if (h.Mana == 0) continue;

                //uint HPX = h.Health * 100 / h.MaximumHealth;
                //string buff = Convert.ToString(HPX);
                
                if (Menu.Item("HPBarShow").GetValue<bool>())
                {
                    Drawing.DrawRect(Pos + new Vector2(0, BPos), new Vector2(PosX, 7), new Color(0, 0, 0, 100));
                    Drawing.DrawRect(Pos + new Vector2(0, BPos), new Vector2(PosHealth.X, 7), new Color(0, 255, 0, 255));
                    Drawing.DrawRect(Pos + new Vector2(0, BPos), new Vector2(PosX, 7), Color.Black, true);
                    BPos += 7;
                }

                if (Menu.Item("ManaBarShow").GetValue<bool>())
                {
                    Drawing.DrawRect(Pos + new Vector2(0, BPos), new Vector2(PosX, 7), new Color(0, 0, 0, 100));
                    Drawing.DrawRect(Pos + new Vector2(0, BPos), new Vector2(PosMana.X, 7), new Color(0, 122, 204, 255));
                    Drawing.DrawRect(Pos + new Vector2(0, BPos), new Vector2(PosX, 7), Color.Black, true);
                }
                // Ebala...
                //Drawing.DrawText(string.Format("({0}% | 999%)", buff), "Arial", Pos + new Vector2(0, PosY + 18), new Vector2(13, 5), Color.White, 0);
            }
        }

    }
}
