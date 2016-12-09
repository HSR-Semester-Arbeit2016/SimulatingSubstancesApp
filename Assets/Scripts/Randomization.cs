using System;
using Assets.Scripts.Helpers;
using Assets.Scripts.MetaData;
using Assets.Scripts.MetaData.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

namespace Assets.Scripts
{
    public class Randomization : MonoBehaviour
    {
        private const int predefinedInterval = 10;
        private const float blurValueMax = 10;
        private const float tunnelValueMax = 15;
        private const float valueMin = 0;
        private float blurLevelCurrent;
        private Randomizer blurRandomizer;
        private long internalTime;
        private float tunnelLevelCurrent;
        private Randomizer tunnelRandomizer;

        private BlurOptimized[] blurComponents;
        private AlcoholTiltShift[] tunnelVisionComponents;

        private Text tunnelValueText;
        private Text blurValueText;

        private void Start()
        {
            internalTime = DateTime.Now.Ticks;
            blurLevelCurrent = PlayerPrefs.GetFloat(PlayerPreferences.BlurLevel);
            blurRandomizer = new Randomizer(blurLevelCurrent, valueMin, blurValueMax, 1, 3000, 8);
            tunnelLevelCurrent = PlayerPrefs.GetFloat(PlayerPreferences.TunnelLevel);
            tunnelRandomizer = new Randomizer(tunnelLevelCurrent, valueMin, tunnelValueMax, 1.5f);

            blurComponents = new BlurOptimized[2];
            FillComponents(blurComponents);
            tunnelVisionComponents = new AlcoholTiltShift[2];
            FillComponents(tunnelVisionComponents);

            tunnelValueText = GameObject.Find(SimulatingSubstancesControls.TunnelLevelText).GetComponent<Text>();
            blurValueText = GameObject.Find(SimulatingSubstancesControls.BlurLevelText).GetComponent<Text>();
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
            blurComponents[0].blurSize = newValue;
            blurComponents[1].blurSize = newValue;
            blurValueText.text = newValue.ToString();
        }

        private void UpdateTunnelValue(float newValue)
        {
            tunnelVisionComponents[0].blurArea = newValue;
            tunnelVisionComponents[1].blurArea = newValue;
            tunnelValueText.text = newValue.ToString();
        }

        private void FillComponents<T>(T[] componentArray)
        {
            componentArray[0] = GameObject.Find(SimulatingSubstancesControls.StereoCameraLeft).GetComponent<T>();
            componentArray[1] = GameObject.Find(SimulatingSubstancesControls.StereoCameraRight).GetComponent<T>();
        }
    }
}