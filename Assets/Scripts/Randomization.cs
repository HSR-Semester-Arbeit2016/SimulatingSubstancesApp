﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.MetaData.UI;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using Random = System.Random;

public class Randomization : MonoBehaviour {

    
    private static int predefinedInterval = 10;
    private static float halvedStayRange = 1500;
    private static float blurValueMax = 10;
    private static float tunnelValueMax = 15;
    private float blurLevelInitial;
    private float blurLevelCurrent;
    private float blurRangeUpper;
    private float blurRangeLower;
    private float tunnelLevelInitial;
    private float tunnelLevelCurrent;
    private float tunnelRangeUpper;
    private float tunnelRangeLower;
    private Random rnd;
    private long internalTime;

    //TODO: Refactor randomization/randomWalk into own class without using UI-Controls

    // Use this for initialization
    void Start ()
	{
        rnd = new Random();
        internalTime = DateTime.Now.Ticks;
        blurLevelInitial = PlayerPrefs.GetFloat("BlurLevel");
	    blurLevelCurrent = blurLevelInitial;
        blurRangeUpper = GetUpperRangeValue(blurLevelInitial, 1, blurValueMax);
	    blurRangeLower = GetLowerRangeValue(blurLevelInitial, 1, 0);

	    tunnelLevelInitial = PlayerPrefs.GetFloat("TunnelLevel");
	    tunnelLevelCurrent = tunnelLevelInitial;
	    tunnelRangeUpper = GetUpperRangeValue(tunnelLevelInitial, 1.5f, tunnelValueMax);
	    tunnelRangeLower = GetLowerRangeValue(tunnelLevelInitial, 1.5f, 0);
#if DEBUG
        Debug.Log("Randomization initialized!");
#endif
    }
	
	// Update is called once per frame
	void Update ()
	{
        var timeSpanElapsed = new TimeSpan(DateTime.Now.Ticks - internalTime);
        var hasTimeElapsed = timeSpanElapsed.TotalSeconds >= predefinedInterval;

        if (enabled && hasTimeElapsed)
        {
            blurLevelCurrent = DoRandomWalk(blurLevelCurrent, blurRangeLower, blurRangeUpper);
            UpdateBlurValue(blurLevelCurrent);
            tunnelLevelCurrent = DoRandomWalk(tunnelLevelCurrent, tunnelRangeLower, tunnelRangeUpper);
            UpdateTunnelValue(tunnelLevelCurrent);

            internalTime = DateTime.Now.Ticks;
        }
    }

    private float GetUpperRangeValue(float currentValue, float increment, float maxValue)
    {
        var upperRange = currentValue + increment;
        if (blurRangeUpper > maxValue)
            upperRange = maxValue;
        return upperRange;
    }

    private float GetLowerRangeValue(float currentValue, float decrement, float minValue)
    {
        var lowerRange = currentValue - decrement;
        if (blurRangeUpper < minValue)
            lowerRange = minValue;
        return lowerRange;
    }

    private float DoRandomWalk(float currentLevel, float rangeLower, float rangeUpper)
    {
        currentLevel += GetRandomWalkDirection(currentLevel, rangeLower, rangeUpper);
        return currentLevel;
    }

    private void UpdateBlurValue(float newValue)
    {
        BlurOptimized leftComponent = GameObject.Find(SimulatingSubstancesControls.StereoCameraLeft).GetComponent<BlurOptimized>();
        BlurOptimized rightComponent = GameObject.Find(SimulatingSubstancesControls.StereoCameraRight).GetComponent<BlurOptimized>();
        leftComponent.blurSize = newValue;
        rightComponent.blurSize = newValue;
        SetEffectValueText(newValue, SimulatingSubstancesControls.BlurLevelText);
#if DEBUG
        Debug.Log(String.Format("Randomization: BlurValue was {0} and is now {1}", blurLevelInitial, newValue));
#endif
    }

    private void UpdateTunnelValue(float newValue)
    {
        AlcoholTiltShift leftComponent = GameObject.Find(SimulatingSubstancesControls.StereoCameraLeft).GetComponent<AlcoholTiltShift>();
        AlcoholTiltShift rightComponent = GameObject.Find(SimulatingSubstancesControls.StereoCameraRight).GetComponent<AlcoholTiltShift>();
        leftComponent.blurArea = newValue;
        rightComponent.blurArea = newValue;
        SetEffectValueText(newValue, SimulatingSubstancesControls.TunnelLevelText);
#if DEBUG
        Debug.Log(String.Format("Randomization: tunnelValue was {0} and is now {1}", tunnelLevelInitial, newValue));
#endif
    }

    private void SetEffectValueText(float value, String textFieldName)
    {
        Text textComponent = GameObject.Find(textFieldName).GetComponent<Text>();
        textComponent.text = value.ToString();
    }

    private float GetRandomWalkDirection(float currentLevel, float rangeLowerBound, float rangeUpperBound)
    {
        float stepValue = (rangeUpperBound - rangeLowerBound)/6;
        int randomValue = rnd.Next(0, 9999);
        float currentLevelNormalized = (currentLevel - rangeLowerBound)/(rangeUpperBound - rangeLowerBound)*10000;
        
        if (randomValue < currentLevelNormalized - halvedStayRange)
            return -stepValue;
        else if (randomValue > currentLevelNormalized + halvedStayRange)
            return stepValue;
        else
            return 0;

    }
}
