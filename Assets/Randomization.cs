using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;
using Random = System.Random;

public class Randomization : MonoBehaviour {

    public Boolean enabled { get; set; }
    private int internalTime;
    private int predefinedInterval;
    private Random rnd;

	// Use this for initialization
	void Start ()
	{
        rnd = new Random();
	    enabled = false;
	    predefinedInterval = 3000;
	    internalTime = DateTime.Now.Millisecond;
	}
	
	// Update is called once per frame
	void Update ()
	{
        var timeElapsed = DateTime.Now.Millisecond >= internalTime + predefinedInterval;
        if (enabled && timeElapsed)
        {
            float currentBlurLevel = PlayerPrefs.GetFloat("BlurLevel");

            if (GetRandomWalkDirection(currentBlurLevel, PlayerPrefs.GetInt("BlurRangeLower"),
                PlayerPrefs.GetInt("BlurRangeUpper")))
            {
                currentBlurLevel++;
            }
            else
                currentBlurLevel--;
            PlayerPrefs.SetFloat("BlurLevel", currentBlurLevel);
            //UpdateComponents
            BlurOptimized cameraLeftBlur = GameObject.Find("StereoCameraLeft").GetComponent<BlurOptimized>();
            BlurOptimized cameraRightBlur = GameObject.Find("StereoCameraRight").GetComponent<BlurOptimized>();
            cameraLeftBlur.blurSize = currentBlurLevel;
            cameraRightBlur.blurSize = currentBlurLevel;

            internalTime = DateTime.Now.Millisecond;
        }
    }


    private Boolean GetRandomWalkDirection(float currentLevel, int rangeLowerBound, int rangeUpperBound)
    {
        int randomValue = rnd.Next(0, 99);
        return randomValue < (currentLevel - rangeLowerBound)/(rangeUpperBound - rangeLowerBound)*100;
    }
}
