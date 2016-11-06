using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityStandardAssets.ImageEffects;

public class SimulatingViewModel : MonoBehaviour
{

	private float timeLeft = 180.0f;

	void Start ()
	{
		this.UpdateBlurValue (PlayerPrefs.GetFloat ("BlurLevel"));
		this.UpdateTunnelValue (PlayerPrefs.GetFloat ("TunnelLevel"));
		this.UpdateDelay (PlayerPrefs.GetInt ("DelayLevel"));
		this.UpdateRedColorDistorsion (PlayerPrefs.GetInt ("RedColorDistorsion"));
		this.UpdateRandomEffects (PlayerPrefs.GetInt ("RandomEffects"));
	}

	private void UpdateBlurValue (float blurValue)
	{
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

	private void UpdateTunnelValue (float tunnelValue)
	{
		
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

	private void UpdateDelay (float delayValue)
	{
		
		if (delayValue > 0) {
			Delay delayLeft = GameObject.Find ("StereoCameraLeft").GetComponent<Delay> ();
			Delay delayRight = GameObject.Find ("StereoCameraRight").GetComponent<Delay> ();
			delayRight.IsEnabled = true;
			delayLeft.IsEnabled = true;
			this.UpdateToggle ("DelayToggle");
		}
	}

	private void UpdateRedColorDistorsion (int redValue)
	{
		
		if (redValue > 0) {
			ColorCorrectionCurves colorCorrectionCurvesLeft = GameObject.Find ("StereoCameraLeft").GetComponent<ColorCorrectionCurves> ();
			ColorCorrectionCurves colorCorrectionCurvesRight = GameObject.Find ("StereoCameraRight").GetComponent<ColorCorrectionCurves> ();
			this.UpdateToggle ("RedColorToggle");
			colorCorrectionCurvesLeft.enabled = true;
			colorCorrectionCurvesRight.enabled = true;
		}
	}

	private void UpdateRandomEffects (int randomEffects)
	{
		if (randomEffects == 1) {
			this.UpdateToggle ("RandomToggle");
			// TODO complete. We need a timer in order to trigger the changes in the different image effects
			Debug.Log ("UpdateRandomEffects called");

		}
		if (randomEffects == 0) {

		}
	}

	private void GenerateRandomBlurEffects ()
	{
		if (PlayerPrefs.GetFloat ("BlurLevel") > 0) {
			int randomBlurLevel = this.GetRandomIntFromRange (0, 4);
			Debug.Log ("GEnerateRandomBlurEffects called with: " + randomBlurLevel);
			//TODO complete this
		} 


	}

	private int GetRandomIntFromRange (int from, int to)
	{
		System.Random rnd = new System.Random ();
		return rnd.Next (from, to);
	}


	private void UpdateToggle (String toggleName)
	{
		Toggle toggle = GameObject.Find (toggleName).GetComponent<Toggle> ();
		toggle.isOn = true;
	}
}
