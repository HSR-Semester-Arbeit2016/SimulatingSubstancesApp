using System;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class Randomization : MonoBehaviour
{
    private static readonly int predefinedInterval = 10;
    private static readonly float blurValueMax = 10;
    private static readonly float tunnelValueMax = 15;

    private BlurOptimized[] blurComponents;
    private float blurLevelCurrent;
    private Randomizer blurRandomizer;
    private Text blurValueText;
    private long internalTime;
    private float tunnelLevelCurrent;
    private Randomizer tunnelRandomizer;

    private Text tunnelValueText;
    private AlcoholTiltShift[] tunnelVisionComponents;

    private void Start()
    {
        internalTime = DateTime.Now.Ticks;
        blurRandomizer = new Randomizer(PlayerPrefs.GetFloat("BlurLevel"), 0, blurValueMax, 1, 3000, 8);
        tunnelRandomizer = new Randomizer(PlayerPrefs.GetFloat("TunnelLevel"), 0, tunnelValueMax, 1.5f);

        blurComponents = new BlurOptimized[2];
        FillComponents(blurComponents);
        tunnelVisionComponents = new AlcoholTiltShift[2];
        FillComponents(tunnelVisionComponents);

        tunnelValueText = GameObject.Find("TunnelLevelText").GetComponent<Text>();
        blurValueText = GameObject.Find("BlurLevelText").GetComponent<Text>();

#if DEBUG
        Debug.Log("Randomization initialized!");
#endif
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
        componentArray[0] = GameObject.Find("StereoCameraLeft").GetComponent<T>();
        componentArray[1] = GameObject.Find("StereoCameraRight").GetComponent<T>();
    }
}