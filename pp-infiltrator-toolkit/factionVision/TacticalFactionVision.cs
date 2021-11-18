using Harmony;
using PhoenixPoint.Common.Entities.Items;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.Entities.Equipments;
using PhoenixPoint.Tactical.Levels;
using PhoenixPoint.Tactical.Levels.Mist;
using System.Text;
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

    [HarmonyPatch(typeof(TacticalFactionVision), "ReUpdateVisibilityTowardsActorImpl")]
    class ReUpdateVisibilityTowardsActorImpl
    {
        [HarmonyPrefix]
        private static bool Prefix(TacticalActorBase fromActor, ref bool __result)
        {
            if (fromActor is ItemContainer)
            {
                // Avoid being detected by the root of a destroyed "egg"
                InventoryComponent inventory = fromActor.Inventory;
                if (inventory.Items.Count <= 0)
                {
                    __result = false;
                    return false;
                }
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(TacticalFactionVision), "GatherKnowableActors")]
    class GatherKnowableActors
    {
        [HarmonyPrefix]
        private static bool Prefix(TacticalActorBase fromActor)
        {
            if (fromActor is ItemContainer)
            {
                // Avoid being detected by the root of a destroyed "egg"
                InventoryComponent inventory = fromActor.Inventory;
                if (inventory.Items.Count <= 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
