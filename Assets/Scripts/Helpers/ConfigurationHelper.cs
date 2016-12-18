using Assets.Scripts.MetaData;
using UnityEngine;

namespace Assets.Scripts.Helpers
{   
    public static class ConfigurationHelper
    {
        public static void ResetConfigEffectValues(Configuration configuration)
        {
            configuration.BlurLevel = 0;
            configuration.TunnelLevel = 0;
            configuration.Delay = 0;
            configuration.MotionBlur = 0;
            configuration.RedColor = 0;
            configuration.Randomization = 0;
        }
		/// <summary>
		/// Returns the default configuration corresponding to a Sober, 
		/// Slightly Drunk, Drunk or Very Drunk alcohol level (key)
		/// </summary>
		/// <returns>The default config.</returns>
		/// <param name="key">Key.</param>
        public static Configuration GetDefaultConfig(string key)
        {
            switch (key)
            {
                case ConfigurationNames.VeryDrunk:
                    return DefaultConfigurations.VeryDrunk;
                case ConfigurationNames.Drunk:
                    return DefaultConfigurations.Drunk;
                case ConfigurationNames.SlightlyDrunk:
                    return DefaultConfigurations.SlightlyDrunk;
                default:
                    return DefaultConfigurations.Sober;
            }
        }
		/// <summary>
		/// Returns a Configuration Object filled with the data obtained from the current values
		/// saved at the PlayerPrefs (see Unity Doc for PlayerPrefs).
		/// </summary>
		/// <returns>The configuration by player prefs.</returns>
		/// <param name="configName">Config name.</param>
        public static Configuration GenerateConfigurationByPlayerPrefs(string configName)
        {
            return new Configuration
            {
                Name = configName,
                BlurLevel = PlayerPrefs.GetFloat(PlayerPreferences.BlurLevel),
                TunnelLevel = PlayerPrefs.GetFloat(PlayerPreferences.TunnelLevel),
                Delay = PlayerPrefs.GetInt(PlayerPreferences.DelayLevel),
                MotionBlur = PlayerPrefs.GetInt(PlayerPreferences.MotionBlur),
                RedColor = PlayerPrefs.GetInt(PlayerPreferences.RedColorDistortion),
                Randomization = PlayerPrefs.GetInt(PlayerPreferences.Randomization)
            };
        }
    }
}