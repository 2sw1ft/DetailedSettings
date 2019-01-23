using System.IO;
using System.Reflection;
using ModSettings;
using UnityEngine;

namespace DetailedSettingsTweaks
{
    public class DetailedSettingsOptions
    {
        public static float FreezingRateScale = 1f;
        public static float FatigueRateScale = 1f;
        public static float ThirstRateScale = 1f;
        public static float CalorieRateScale = 1f;
        public static float SprintStaminaUsagePerSecond = 1f;
        public static float WindMovementMultiplier = 1f;
    }

    class DetailedSettingsTweaks
    {
        private static readonly string mod_options_folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static readonly string options_file_name = Path.Combine(mod_options_folder, "DetailedSettings.json");

        public static void OnLoad()
        {
            Debug.Log("[TempDrop] Version " + Assembly.GetExecutingAssembly().GetName().Version);
        }

        internal class DetailedSettingsSettings : ModSettingsBase
        {
            [Section("Needs")]
            [Name("Rate of Freezing")]
            [Description("Set how fast the freezing of the player decrease. 0 is turn off, 1 is default value, 2 is increased rate")]
            [Slider(0f, 2f)]
            public float FreezingRateScale = 1f;

            [Name("Rate of fatigue")]
            [Description("Set how fast the fatigue of the player decrease. 0 is turn off, 1 is default value, 2 is increased rate")]
            [Slider(0f, 2f)]
            public float FatigueRateScale = 1f;

            [Name("Rate of thirst")]
            [Description("Set how fast the thirst of the player decrease. 0 is turn off, 1 is default value, 2 is increased rate")]
            [Slider(0f, 2f)]
            public float ThirstRateScale = 1f;

            [Name("Rate of calories")]
            [Description("Set how fast the calories of the player decrease. 0 is turn off, 1 is default value, 2 is increased rate")]
            [Slider(0f, 2f)]
            public float CalorieRateScale = 1f;

            [Section("Condition")]
            [Name("Sprint stamina")]
            [Description("Set how fast the stamina of the player while running decrease. 0 is turn off, 5 is default value, 10 is increased rate")]
            [Slider(0f, 10f)]
            public float SprintStaminaUsagePerSecond = 5f;

            [Name("Wind movement multiplier")]
            [Description("The effect of headwinds on player speed. 1 - no effect at all, 0 - the player is barely moving.")]
            [Slider(0f, 1f)]
            public float WindMovementMultiplier = 1f;

            protected override void OnConfirm()
            {
                DetailedSettingsOptions.FreezingRateScale = FreezingRateScale;
                DetailedSettingsOptions.FatigueRateScale = FatigueRateScale;
                DetailedSettingsOptions.ThirstRateScale = ThirstRateScale;
                DetailedSettingsOptions.CalorieRateScale = CalorieRateScale;
                DetailedSettingsOptions.SprintStaminaUsagePerSecond = SprintStaminaUsagePerSecond;
                DetailedSettingsOptions.WindMovementMultiplier = WindMovementMultiplier;

                string json_opts = FastJson.Serialize(this);

                File.WriteAllText(Path.Combine(mod_options_folder, options_file_name), json_opts);
            }
        }

        internal static class DetailedSettingsLoad
        {
            private static DetailedSettingsSettings custom_settings = new DetailedSettingsSettings();

            public static void OnLoad()
            {
                if (File.Exists(Path.Combine(mod_options_folder, options_file_name)))
                {
                    string opts = File.ReadAllText(Path.Combine(mod_options_folder, options_file_name));
                    custom_settings = FastJson.Deserialize<DetailedSettingsSettings>(opts);

                    DetailedSettingsOptions.FreezingRateScale = custom_settings.FreezingRateScale;
                    DetailedSettingsOptions.FatigueRateScale = custom_settings.FatigueRateScale;
                    DetailedSettingsOptions.ThirstRateScale = custom_settings.ThirstRateScale;
                    DetailedSettingsOptions.CalorieRateScale = custom_settings.CalorieRateScale;
                    DetailedSettingsOptions.SprintStaminaUsagePerSecond = custom_settings.SprintStaminaUsagePerSecond;
                    DetailedSettingsOptions.WindMovementMultiplier = custom_settings.WindMovementMultiplier;
                }

                custom_settings.AddToModSettings("Detailed Settings");
            }
        }
    }
}
