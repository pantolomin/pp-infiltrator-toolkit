using Harmony;
using PhoenixPoint.Tactical.Levels;

namespace phoenix_point.mod.infiltrator_toolkit.factionVision
{
    [HarmonyPatch(typeof(TacticalFactionVision), "OnLevelStart")]
    class OnLevelStart
    {
        [HarmonyPostfix]
        private static void Postfix(TacticalFactionVision __instance)
        {
            Indicators.DeclareFaction(__instance);
        }
    }
}
