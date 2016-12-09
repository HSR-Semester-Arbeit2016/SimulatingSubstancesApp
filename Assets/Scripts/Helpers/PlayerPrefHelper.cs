using Assets.Scripts.MetaData;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public static class PlayerPrefHelper
    {
        public static void ResetPlayerPrefs()
        {
            PlayerPrefs.SetFloat(PlayerPreferences.BlurLevel, 0);
            PlayerPrefs.SetFloat(PlayerPreferences.TunnelLevel, 0);
            PlayerPrefs.SetInt(PlayerPreferences.DelayLevel, 0);
            PlayerPrefs.SetInt(PlayerPreferences.MotionBlur, 0);
            PlayerPrefs.SetInt(PlayerPreferences.RedColorDistortion, 0);
            PlayerPrefs.SetInt(PlayerPreferences.Randomization, 0);
        }
    }
}