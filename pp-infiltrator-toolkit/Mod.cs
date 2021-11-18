using Harmony;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace phoenix_point.mod.infiltrator_toolkit
{
    public class Mod
    {
        internal static ModConfig Config;

        public static void Init()
        {
            new Mod().MainMod();
        }

        public void MainMod(Func<string, object, object> api = null)
        {
            Config = api("config", null) as ModConfig ?? new ModConfig();
            HarmonyInstance.Create("phoenixpoint.InfiltratorToolkit").PatchAll(Assembly.GetExecutingAssembly());
            api("log verbose", "Mod Initialised.");
        }
    }
}
