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

    [HarmonyPatch(typeof(TacticalFactionVision), "CheckVisibleLine", typeof(TacticalActorBase), typeof(Vector3), typeof(Vector3), typeof(float), typeof(bool))]
    class CheckVisibleLine
    {
        internal static float currentRangeMultiplier;

        [HarmonyPrefix]
        private static void Prefix(float rangeMultiplier)
        {
            currentRangeMultiplier = rangeMultiplier;
        }
    }

    [HarmonyPatch(typeof(TacticalFactionVision), "CastVisiblilityLineCheck")]
    class CastVisiblilityLineCheck
    {
        [HarmonyPrefix]
        private static void Prefix(ref float perceptionRange)
        {
            float minPerception = Mod.GetMinPerception();
            if (CheckVisibleLine.currentRangeMultiplier < 0f)
            {
                FileLog.Log("CheckVisibleLine.currentRangeMultiplier: " + CheckVisibleLine.currentRangeMultiplier);
                minPerception *= 1f + CheckVisibleLine.currentRangeMultiplier;
            }
            perceptionRange = Mathf.Max(minPerception, perceptionRange);
        }
    }
}
