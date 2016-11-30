using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityStandardAssets.ImageEffects;

public class SimulatingViewModel : MonoBehaviour
{


	void Start ()
	{
		this.UpdateBlurValue (PlayerPrefs.GetFloat ("BlurLevel"));
		this.UpdateTunnelValue (PlayerPrefs.GetFloat ("TunnelLevel"));
		this.UpdateDelay (PlayerPrefs.GetInt ("DelayLevel"));
		this.UpdateMotionBlur (PlayerPrefs.GetInt ("MotionBlur"));
		this.UpdateRedColorDistortion (PlayerPrefs.GetInt ("RedColorDistortion"));
		this.UpdateRandomEffects (PlayerPrefs.GetInt ("RandomEffects"));
	}

	private void UpdateBlurValue (float value)
	{
		if (value > 0) {
			Text blurValueText = GameObject.Find ("BlurLevelText").GetComponent<Text> ();
			blurValueText.text = value.ToString ();
			BlurOptimized cameraLeftBlur = GameObject.Find ("StereoCameraLeft").GetComponent<BlurOptimized> ();
			BlurOptimized cameraRightBlur = GameObject.Find ("StereoCameraRight").GetComponent<BlurOptimized> ();
			cameraLeftBlur.blurSize = value;
			cameraRightBlur.blurSize = value;
			this.UpdateToggle ("BlurToggle");
		}
	}

	private void UpdateTunnelValue (float value)
	{
		if (value > 0) {
			Text tunnelValueText = GameObject.Find ("TunnelLevelText").GetComponent<Text> ();
			tunnelValueText.text = value.ToString ();
			AlcoholTiltShift cameraLeftTunnel = GameObject.Find ("StereoCameraLeft").GetComponent<AlcoholTiltShift> ();			
			AlcoholTiltShift cameraRightTunnel = GameObject.Find ("StereoCameraRight").GetComponent<AlcoholTiltShift> ();
			cameraLeftTunnel.blurArea = value;
			cameraRightTunnel.blurArea = value;
			this.UpdateToggle ("TunnelToggle");
		}
	}

	private void UpdateDelay (float value)
	{
		
		if (value > 0) {
			Delay delayLeft = GameObject.Find ("StereoCameraLeft").GetComponent<Delay> ();
			Delay delayRight = GameObject.Find ("StereoCameraRight").GetComponent<Delay> ();
			delayLeft.IsEnabled = true;
			delayRight.IsEnabled = true;
			this.UpdateToggle ("DelayToggle");
		}
	}

	private void UpdateMotionBlur (int value)
	{
		if (value > 0) {
			CameraMotionBlur motionBlurLeft = GameObject.Find ("StereoCameraLeft").GetComponent<CameraMotionBlur> ();
			CameraMotionBlur motionBlurRight = GameObject.Find ("StereoCameraRight").GetComponent<CameraMotionBlur> ();
			motionBlurLeft.enabled = true;
			motionBlurRight.enabled = true;
			this.UpdateToggle ("MotionBlurToggle");
		}
	}

	private void UpdateRedColorDistortion (int value)
	{
		if (value > 0) {
			ColorCorrectionCurves colorCorrectionCurvesLeft = GameObject.Find ("StereoCameraLeft").GetComponent<ColorCorrectionCurves> ();
			ColorCorrectionCurves colorCorrectionCurvesRight = GameObject.Find ("StereoCameraRight").GetComponent<ColorCorrectionCurves> ();
			colorCorrectionCurvesLeft.enabled = true;
			colorCorrectionCurvesRight.enabled = true;
			this.UpdateToggle ("RedColorToggle");
		}
	}

	private void UpdateRandomEffects (int value)
	{
		if (value > 0) {
		    Randomization randomization = GameObject.Find("ARCamera").GetComponent<Randomization>();
		    randomization.enabled = true;
            this.UpdateToggle("RandomToggle");
        }
	}

	private void UpdateToggle (String toggleName)
	{
		Toggle toggle = GameObject.Find (toggleName).GetComponent<Toggle> ();
		toggle.isOn = true;
	}
}
