using Base.Entities;
using Harmony;
using PhoenixPoint.Common.UI;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.View;
using System.Linq;
using UnityEngine;

namespace phoenix_point.mod.infiltrator_toolkit.healthbar
{
    [HarmonyPatch(typeof(HealthbarUIActorElement), "InitHealthbar")]
    public class InitHealthbar
    {
        [HarmonyPostfix]
        private static void Postfix(HealthbarUIActorElement __instance, ActorComponent actor)
        {
            TacticalActorBase tacActor = actor as TacticalActorBase;
            ViewElementDef viewElementDef = tacActor.ClassViewElementDefs.ElementAt<ViewElementDef>(0);
            Sprite smallIcon = viewElementDef.SmallIcon ?? viewElementDef.LargeIcon;
            if (smallIcon != null)
            {
                Indicators.InitIndicators(
                    (RectTransform)__instance.ActorClassIconElement.MainClassIcon.transform,
                    tacActor,
                    smallIcon);
            }
        }
    }
}
