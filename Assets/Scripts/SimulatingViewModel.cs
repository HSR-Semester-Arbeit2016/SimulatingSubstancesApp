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
	}

	private void UpdateBlurValue ()
	{
		float blurValue = PlayerPrefs.GetFloat ("BlurLevel");
		Text blurValueText = GameObject.Find ("BlurLevelText").GetComponent<Text> ();
		blurValueText.text = blurValue.ToString ();
		BlurOptimized cameraLeftBlur = GameObject.Find ("StereoCameraLeft").GetComponent<BlurOptimized> ();
		cameraLeftBlur.blurSize = blurValue;
		BlurOptimized cameraRightBlur = GameObject.Find ("StereoCameraRight").GetComponent<BlurOptimized> ();
		cameraRightBlur.blurSize = blurValue;
		if (blurValue > 0) {
			this.UpdateToggle ("BlurToggle");
		}
	}

	private void UpdateTunnelValue ()
	{
		float tunnelValue = PlayerPrefs.GetFloat ("TunnelLevel");
		Text tunnelValueText = GameObject.Find ("TunnelLevelText").GetComponent<Text> ();
		tunnelValueText.text = tunnelValue.ToString ();
		AlcoholTiltShift cameraLeftTunnel = GameObject.Find ("StereoCameraLeft").GetComponent<AlcoholTiltShift> ();
		cameraLeftTunnel.blurArea = tunnelValue;
		AlcoholTiltShift cameraRightTunnel = GameObject.Find ("StereoCameraRight").GetComponent<AlcoholTiltShift> ();
		cameraRightTunnel.blurArea = tunnelValue;
		if (tunnelValue > 0) {
			this.UpdateToggle ("TunnelToggle");
		}
	}


	private void UpdateToggle (String toggleName)
	{
		Toggle toggle = GameObject.Find (toggleName).GetComponent<Toggle> ();
		toggle.isOn = true;
	}
}
