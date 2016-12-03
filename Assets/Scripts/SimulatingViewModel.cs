using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Assets.Scripts.MetaData;
using Assets.Scripts.MetaData.UI;
using UnityStandardAssets.ImageEffects;

public class SimulatingViewModel : MonoBehaviour
{


	void Start ()
	{
		UpdateBlurValue (PlayerPrefs.GetFloat (PlayerPreferences.BlurLevel));
		UpdateTunnelValue (PlayerPrefs.GetFloat (PlayerPreferences.TunnelLevel));
		UpdateDelay (PlayerPrefs.GetInt (PlayerPreferences.DelayLevel));
		UpdateMotionBlur (PlayerPrefs.GetInt (PlayerPreferences.MotionBlur));
		UpdateRedColorDistortion (PlayerPrefs.GetInt (PlayerPreferences.RedColorDistortion));
		UpdateRandomEffects (PlayerPrefs.GetInt (PlayerPreferences.Randomization));
	}

	private void UpdateBlurValue (float value)
	{
		if (value > 0) {
			Text blurValueText = GameObject.Find (SimulatingSubstancesControls.BlurLevelText).GetComponent<Text> ();
			blurValueText.text = value.ToString ();
			BlurOptimized cameraLeftBlur = GameObject.Find (SimulatingSubstancesControls.StereoCameraLeft).GetComponent<BlurOptimized> ();
			BlurOptimized cameraRightBlur = GameObject.Find (SimulatingSubstancesControls.StereoCameraRight).GetComponent<BlurOptimized> ();
			cameraLeftBlur.blurSize = value;
			cameraRightBlur.blurSize = value;
			UpdateToggle (SimulatingSubstancesControls.BlurToggle);
		}
	}

	private void UpdateTunnelValue (float value)
	{
		if (value > 0) {
			Text tunnelValueText = GameObject.Find (SimulatingSubstancesControls.TunnelLevelText).GetComponent<Text> ();
			tunnelValueText.text = value.ToString ();
			AlcoholTiltShift cameraLeftTunnel = GameObject.Find (SimulatingSubstancesControls.StereoCameraLeft).GetComponent<AlcoholTiltShift> ();			
			AlcoholTiltShift cameraRightTunnel = GameObject.Find (SimulatingSubstancesControls.StereoCameraRight).GetComponent<AlcoholTiltShift> ();
			cameraLeftTunnel.blurArea = value;
			cameraRightTunnel.blurArea = value;
			this.UpdateToggle (SimulatingSubstancesControls.TunnelToggle);
		}
	}

	private void UpdateDelay (float value)
	{
		
		if (value > 0) {
			Delay delayLeft = GameObject.Find (SimulatingSubstancesControls.StereoCameraLeft).GetComponent<Delay> ();
			Delay delayRight = GameObject.Find (SimulatingSubstancesControls.StereoCameraRight).GetComponent<Delay> ();
			delayLeft.enabled = true;
			delayRight.enabled = true;
			this.UpdateToggle (SimulatingSubstancesControls.DelayToggle);
		}
	}

	private void UpdateMotionBlur (int value)
	{
		if (value > 0) {
			CameraMotionBlur motionBlurLeft = GameObject.Find (SimulatingSubstancesControls.StereoCameraLeft).GetComponent<CameraMotionBlur> ();
			CameraMotionBlur motionBlurRight = GameObject.Find (SimulatingSubstancesControls.StereoCameraRight).GetComponent<CameraMotionBlur> ();
			motionBlurLeft.enabled = true;
			motionBlurRight.enabled = true;
			this.UpdateToggle (SimulatingSubstancesControls.MotionBlurToggle);
		}
	}

	private void UpdateRedColorDistortion (int value)
	{
		if (value > 0) {
			ColorCorrectionCurves colorCorrectionCurvesLeft = GameObject.Find (SimulatingSubstancesControls.StereoCameraLeft).GetComponent<ColorCorrectionCurves> ();
			ColorCorrectionCurves colorCorrectionCurvesRight = GameObject.Find (SimulatingSubstancesControls.StereoCameraRight).GetComponent<ColorCorrectionCurves> ();
			colorCorrectionCurvesLeft.enabled = true;
			colorCorrectionCurvesRight.enabled = true;
			this.UpdateToggle (SimulatingSubstancesControls.RedColorToggle);
		}
	}

	private void UpdateRandomEffects (int value)
	{
		if (value > 0) {
		    Randomization randomization = GameObject.Find(SimulatingSubstancesControls.ARCamera).GetComponent<Randomization>();
		    randomization.enabled = true;
            this.UpdateToggle(SimulatingSubstancesControls.RandomizationToggle);
        }
	}

    //TODO: Possibly extract into UI-Helper
	private void UpdateToggle (String toggleName)
	{
		Toggle toggle = GameObject.Find (toggleName).GetComponent<Toggle> ();
		toggle.isOn = true;
	}
}
