using Harmony;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.Levels;
using UnityEngine;

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

    [HarmonyPatch(typeof(TacticalFactionVision), "CheckVisibleLine")]
    class CheckVisibleLine
    {
        [HarmonyPrefix]
        private static void Prefix(ref float basePerceptionRange)
        {
            basePerceptionRange = Mod.GetMinPerception();
        }
    }
}
