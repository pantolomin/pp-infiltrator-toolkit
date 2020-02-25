using Harmony;
using PhoenixPointModLoader;
using System.Reflection;

namespace phoenix_point.mod.infiltrator_toolkit
{
    public class Mod : IPhoenixPointMod
    {
        public ModLoadPriority Priority => ModLoadPriority.Normal;

        public void Initialize()
        {
            HarmonyInstance.Create("phoenixpoint.InfiltratorToolkit").PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
