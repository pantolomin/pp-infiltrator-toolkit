using Harmony;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.View;

namespace phoenix_point.mod.infiltrator_toolkit.healthbar
{
    [HarmonyPatch(typeof(HealthbarUIActorElement), "LateUpdate")]
    public class LateUpdate
    {
        [HarmonyPostfix]
        private static void Postfix(HealthbarUIActorElement __instance)
        {
            Indicators.UpdateIndicators(
                __instance.ActorClassIconElement,
                __instance.Actor as TacticalActorBase);
        }
    }
}
