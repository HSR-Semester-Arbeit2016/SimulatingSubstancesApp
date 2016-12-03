using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Helpers;
using Assets.Scripts.MetaData.UI;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using Random = System.Random;

public class Randomization : MonoBehaviour {

    
    private static int predefinedInterval = 10;
    private static float blurValueMax = 10;
    private static float tunnelValueMax = 15;
    private long internalTime;
    private float blurLevelCurrent;
    private float tunnelLevelCurrent;
    private Randomizer blurRandomizer;
    private Randomizer tunnelRandomizer;

    void Start ()
	{
        internalTime = DateTime.Now.Ticks;
        blurRandomizer = new Randomizer(PlayerPrefs.GetFloat("BlurLevel"), 0, blurValueMax, 1, 3000, 8);
        tunnelRandomizer = new Randomizer(PlayerPrefs.GetFloat("TunnelLevel"), 0, tunnelValueMax, 1.5f);
#if DEBUG
        Debug.Log("Randomization initialized!");
#endif
    }
	
	void Update ()
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
        BlurOptimized leftComponent = GameObject.Find(SimulatingSubstancesControls.StereoCameraLeft).GetComponent<BlurOptimized>();
        BlurOptimized rightComponent = GameObject.Find(SimulatingSubstancesControls.StereoCameraRight).GetComponent<BlurOptimized>();
        leftComponent.blurSize = newValue;
        rightComponent.blurSize = newValue;
        UiHelper.SetEffectValueText(newValue, SimulatingSubstancesControls.BlurLevelText);
#if DEBUG
        Debug.Log(String.Format("Randomization: BlurValue was {0} and is now {1}", PlayerPrefs.GetFloat("BlurLevel"), newValue));
#endif
    }

    private void UpdateTunnelValue(float newValue)
    {
        AlcoholTiltShift leftComponent = GameObject.Find(SimulatingSubstancesControls.StereoCameraLeft).GetComponent<AlcoholTiltShift>();
        AlcoholTiltShift rightComponent = GameObject.Find(SimulatingSubstancesControls.StereoCameraRight).GetComponent<AlcoholTiltShift>();
        leftComponent.blurArea = newValue;
        rightComponent.blurArea = newValue;
        UiHelper.SetEffectValueText(newValue, SimulatingSubstancesControls.TunnelLevelText);
#if DEBUG
        Debug.Log(String.Format("Randomization: tunnelValue was {0} and is now {1}", PlayerPrefs.GetFloat("TunnelLevel"), newValue));
#endif
    }
}
