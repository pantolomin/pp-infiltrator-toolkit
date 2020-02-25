using Harmony;
using PhoenixPoint.Common.Entities.GameTags;
using PhoenixPoint.Tactical.Entities.Weapons;

namespace phoenix_point.mod.infiltrator_toolkit.weapon
{
    [HarmonyPatch(typeof(Weapon), "IsAttackSilent")]
    class IsAttackSilent
    {
        private const string CROSSBOW = "CrossbowItem_TagDef";

        [HarmonyPrefix]
        private static bool Prefix(Weapon __instance, ref bool __result)
        {
            if (Mod.IsCrossbowSilent())
            {
                foreach (GameTagDef tag in __instance.WeaponDef.Tags)
                {
                    if (CROSSBOW.Equals(tag.name))
                    {
                        __result = true;
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
