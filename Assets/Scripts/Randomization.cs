using System;
using Assets.Scripts.Helpers;
using Assets.Scripts.MetaData.UI;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

namespace Assets.Scripts
{
    public class Randomization : MonoBehaviour
    {
        private static readonly int predefinedInterval = 10;
        private static readonly float blurValueMax = 10;
        private static readonly float tunnelValueMax = 15;
        private float blurLevelCurrent;
        private Randomizer blurRandomizer;
        private long internalTime;
        private float tunnelLevelCurrent;
        private Randomizer tunnelRandomizer;

        private void Start()
        {
            internalTime = DateTime.Now.Ticks;
            blurRandomizer = new Randomizer(PlayerPrefs.GetFloat("BlurLevel"), 0, blurValueMax, 1, 3000, 8);
            tunnelRandomizer = new Randomizer(PlayerPrefs.GetFloat("TunnelLevel"), 0, tunnelValueMax, 1.5f);
            Debug.Log("Randomization initialized!");
        }

        private void Update()
        {
            var timeSpanElapsed = new TimeSpan(DateTime.Now.Ticks - internalTime);
            var hasTimeElapsed = timeSpanElapsed.TotalSeconds >= predefinedInterval;

            if (enabled && hasTimeElapsed)
            {
                blurLevelCurrent = blurRandomizer.DoRandomWalk(blurLevelCurrent);
                UpdateBlurValue(blurLevelCurrent);
                tunnelLevelCurrent = tunnelRandomizer.DoRandomWalk(tunnelLevelCurrent);
                UpdateTunnelValue(tunnelLevelCurrent);

                internalTime = DateTime.Now.Ticks;
            }
        }

        private void UpdateBlurValue(float newValue)
        {
            var leftComponent =
                GameObject.Find(SimulatingSubstancesControls.StereoCameraLeft).GetComponent<BlurOptimized>();
            var rightComponent =
                GameObject.Find(SimulatingSubstancesControls.StereoCameraRight).GetComponent<BlurOptimized>();
            leftComponent.blurSize = newValue;
            rightComponent.blurSize = newValue;
            UiHelper.SetEffectValueText(newValue, SimulatingSubstancesControls.BlurLevelText);
            Debug.Log(string.Format("Randomization: BlurValue was {0} and is now {1}", PlayerPrefs.GetFloat("BlurLevel"),
                newValue));
        }

        private void UpdateTunnelValue(float newValue)
        {
            var leftComponent =
                GameObject.Find(SimulatingSubstancesControls.StereoCameraLeft).GetComponent<AlcoholTiltShift>();
            var rightComponent =
                GameObject.Find(SimulatingSubstancesControls.StereoCameraRight).GetComponent<AlcoholTiltShift>();
            leftComponent.blurArea = newValue;
            rightComponent.blurArea = newValue;
            UiHelper.SetEffectValueText(newValue, SimulatingSubstancesControls.TunnelLevelText);
            Debug.Log(string.Format("Randomization: tunnelValue was {0} and is now {1}",
                PlayerPrefs.GetFloat("TunnelLevel"),
                newValue));
        }
    }
}