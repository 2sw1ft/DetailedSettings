
using Harmony;

namespace DetailedSettingsTweaks
{

    [HarmonyPatch(typeof(PlayerManager), "CalculateModifiedCalorieBurnRate")]
    class DetailedSettingsCalorie
    {
        public static void Postfix(ref float __result)
        {
            __result *= DetailedSettingsOptions.CalorieRateScale;
        }
    }

    [HarmonyPatch(typeof(ExperienceModeManager), "GetThirstRateScale")]
    class DetailedSettingsThirst
    {
        public static void Postfix(ref float __result)
        {
            __result = DetailedSettingsOptions.ThirstRateScale;
        }
    }

    [HarmonyPatch(typeof(ExperienceModeManager), "GetFreezingRateScale")]
    class DetailedSettingsFreezing
    {
        public static void Postfix(ref float __result)
        {
            __result = DetailedSettingsOptions.FreezingRateScale;
        }
    }

    [HarmonyPatch(typeof(ExperienceModeManager), "GetFatigueRateScale")]
    class DetailedSettingsFatigue
    {
        public static void Postfix(ref float __result)
        {
            __result = DetailedSettingsOptions.FatigueRateScale;
        }
    }

    [HarmonyPatch(typeof(PlayerMovement), "Update")]
    class DetailedSettingsSprintStamina
    {
        public static void Prefix(PlayerMovement __instance)
        {
            __instance.m_SprintStaminaUsagePerSecond = DetailedSettingsOptions.SprintStaminaUsagePerSecond;
        }
    }

    [HarmonyPatch(typeof(PlayerMovement), "GetWindMovementMultiplier")]
    class DetailedSettingsWindMovementMultiplier
    {
        public static void Prefix(PlayerMovement __instance)
        {
            __instance.m_WindMovementSpeedMultiplierMin = DetailedSettingsOptions.WindMovementMultiplier;
        }
    }
}
