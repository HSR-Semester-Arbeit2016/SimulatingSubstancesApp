using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityStandardAssets.ImageEffects;

public class SimulatingViewModel : MonoBehaviour
{

	void Start ()
	{
		this.UpdateBlurValue ();
		this.UpdateTunnelValue ();
		this.UpdateDelay ();
		this.UpdateRedColorDistorsion ();
	}

	private void UpdateBlurValue ()
	{
		float blurValue = PlayerPrefs.GetFloat ("BlurLevel");
		if (blurValue > 0) {
			Text blurValueText = GameObject.Find ("BlurLevelText").GetComponent<Text> ();
			blurValueText.text = blurValue.ToString ();
			BlurOptimized cameraLeftBlur = GameObject.Find ("StereoCameraLeft").GetComponent<BlurOptimized> ();
			cameraLeftBlur.blurSize = blurValue;
			BlurOptimized cameraRightBlur = GameObject.Find ("StereoCameraRight").GetComponent<BlurOptimized> ();
			cameraRightBlur.blurSize = blurValue;
			this.UpdateToggle ("BlurToggle");
		}
	}

	private void UpdateTunnelValue ()
	{
		float tunnelValue = PlayerPrefs.GetFloat ("TunnelLevel");
		if (tunnelValue > 0) {
			Text tunnelValueText = GameObject.Find ("TunnelLevelText").GetComponent<Text> ();
			tunnelValueText.text = tunnelValue.ToString ();
			AlcoholTiltShift cameraLeftTunnel = GameObject.Find ("StereoCameraLeft").GetComponent<AlcoholTiltShift> ();
			cameraLeftTunnel.blurArea = tunnelValue;
			AlcoholTiltShift cameraRightTunnel = GameObject.Find ("StereoCameraRight").GetComponent<AlcoholTiltShift> ();
			cameraRightTunnel.blurArea = tunnelValue;
			this.UpdateToggle ("TunnelToggle");
		}
	}

	private void UpdateDelay ()
	{
		float delayValue = PlayerPrefs.GetInt ("DelayLevel");
		if (delayValue > 0) {
			Delay delayLeft = GameObject.Find ("StereoCameraLeft").GetComponent<Delay> ();
			Delay delayRight = GameObject.Find ("StereoCameraRight").GetComponent<Delay> ();
			delayRight.IsEnabled = true;
			delayLeft.IsEnabled = true;
			this.UpdateToggle ("DelayToggle");
		}
	}

	private void UpdateRedColorDistorsion ()
	{
		int redValue = PlayerPrefs.GetInt ("RedColorDistorsion");
		if (redValue > 0) {
			ColorCorrectionCurves colorCorrectionCurvesLeft = GameObject.Find ("StereoCameraLeft").GetComponent<ColorCorrectionCurves> ();
			ColorCorrectionCurves colorCorrectionCurvesRight = GameObject.Find ("StereoCameraRight").GetComponent<ColorCorrectionCurves> ();
			this.UpdateToggle ("RedColorToggle");
			colorCorrectionCurvesLeft.enabled = true;
			colorCorrectionCurvesRight.enabled = true;
		}
	}

	private void UpdateToggle (String toggleName)
	{
		Toggle toggle = GameObject.Find (toggleName).GetComponent<Toggle> ();
		toggle.isOn = true;
	}
}
