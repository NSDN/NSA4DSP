using System;
using System.Threading;

using BepInEx;

namespace NSA4DSP
{
    [BepInPlugin("cn.ac.nya.nsa4dsp", "NSA4DSP", "0.1")]
    [BepInProcess("DSPGAME.exe")]
    public class NSA4DSP : BaseUnityPlugin
    {
        void Awake()
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                while (!DSPGame.MenuDemoLoaded) Thread.Sleep(1);
                while (GameMain.instance == null) Thread.Sleep(1);
                Console.WriteLine("Game Loaded!");
                while (true)
                {
                    if (GameMain.instance != null)
                        if (!GameMain.instance.isMenuDemo)
                            break;
                    Thread.Sleep(1);
                }
                while (!GameMain.instance.running) Thread.Sleep(1);
                Console.WriteLine("Save Loaded!");

                var Data = GameMain.data;
                var History = Data.history;
                var Player = Data.mainPlayer;

                while (Player.movementState != EMovementState.Walk) Thread.Sleep(1);

                Console.WriteLine("Add sands...");
                Player.SetSandCount(Player.sandCount + 100000);

                //Console.WriteLine("Listing items:");
                //foreach (var i in LDB.items.dataArray)
                //{
                //    Console.WriteLine("  " + i.ID + ": " + i.Name);
                //}

            }));
            thread.Start();
            Console.WriteLine("NSA4DSP started.");
        }

    }
}
