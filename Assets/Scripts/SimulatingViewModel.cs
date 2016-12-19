using System;
using System.Collections.Generic;
using Assets.Scripts.Helpers;
using Assets.Scripts.MetaData;
using Assets.Scripts.MetaData.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

namespace Assets.Scripts
{
	/// <summary>
	/// Manages the Simulating Substances scene. This class basically obtains the different GUI 
	/// elements and sets on them the values stored in the PlayerPrefs. 
	/// </summary>
	public class SimulatingViewModel : MonoBehaviour
	{
		//GUI Components references
		//ARCamera components
		private GameObject arCameraComponent;
		private GameObject stereoCameraLeftComponent;
		private GameObject stereoCameraRightComponent;
		//Image effects components belonging to the ARCamera
		private BlurOptimized[] blurComponents;
		private AlcoholTiltShift[] tunnelVisionComponents;
		private Delay[] delayComponents;
		private CameraMotionBlur[] motionBlurComponents;
		private ColorCorrectionCurves[] colorCorrectionComponents;
		private Randomization randomizationComponent;
		//Information Text fields components
		private Text blurValueText;
		private Text tunnelValueText;
		//Toggles
		private Toggle blurToggle;
		private Toggle tunnelToggle;
		private Toggle delayToggle;
		private Toggle motionBlurToggle;
		private Toggle colorDistortionToggle;
		private Toggle randomizationToggle;


		private void Start ()
		{	// Get all the GUI components from GameObject
			arCameraComponent = GameObject.Find (SimulatingSubstancesControls.ARCamera);
			stereoCameraLeftComponent = GameObject.Find (SimulatingSubstancesControls.StereoCameraLeft);
			stereoCameraRightComponent = GameObject.Find (SimulatingSubstancesControls.StereoCameraRight);

			randomizationComponent = arCameraComponent.GetComponent<Randomization> ();
			//ARCamera components are always 2 because there are two stereo cameras, left and right
			blurComponents = new BlurOptimized[2];
			FillComponents (blurComponents);
			tunnelVisionComponents = new AlcoholTiltShift[2];
			FillComponents (tunnelVisionComponents);
			delayComponents = new Delay[2];
			FillComponents (delayComponents);
			motionBlurComponents = new CameraMotionBlur[2];
			FillComponents (motionBlurComponents);
			colorCorrectionComponents = new ColorCorrectionCurves[2];
			FillComponents (colorCorrectionComponents);

			blurValueText = GameObject.Find (SimulatingSubstancesControls.BlurLevelText).GetComponent<Text> ();
			tunnelValueText = GameObject.Find (SimulatingSubstancesControls.TunnelLevelText).GetComponent<Text> ();
			blurToggle = GameObject.Find (SimulatingSubstancesControls.BlurToggle).GetComponent<Toggle> ();
			tunnelToggle = GameObject.Find (SimulatingSubstancesControls.TunnelToggle).GetComponent<Toggle> ();
			delayToggle = GameObject.Find (SimulatingSubstancesControls.DelayToggle).GetComponent<Toggle> ();
			motionBlurToggle = GameObject.Find (SimulatingSubstancesControls.MotionBlurToggle).GetComponent<Toggle> ();
			colorDistortionToggle = GameObject.Find (SimulatingSubstancesControls.RedColorToggle).GetComponent<Toggle> ();
			randomizationToggle = GameObject.Find (SimulatingSubstancesControls.RandomizationToggle).GetComponent<Toggle> ();
			//Update the different GUI elements with values from PlayerPrefs
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
				blurValueText.text = value.ToString ();
				blurComponents [0].blurSize = value;
				blurComponents [1].blurSize = value;
				blurToggle.isOn = true;
			}
		}

		private void UpdateTunnelValue (float value)
		{
			if (value > 0) {
				tunnelValueText.text = value.ToString ();
				tunnelVisionComponents [0].blurArea = value;
				tunnelVisionComponents [1].blurArea = value;
				tunnelToggle.isOn = true;
			}
		}

		private void UpdateDelay (float value)
		{
			if (value > 0) {
				delayComponents [0].IsEnabled = true;
				delayComponents [1].IsEnabled = true;
				delayToggle.isOn = true;
			}
		}

		private void UpdateMotionBlur (int value)
		{
			if (value > 0) {
				motionBlurComponents [0].enabled = true;
				motionBlurComponents [1].enabled = true;
				motionBlurToggle.isOn = true;
			}
		}

		private void UpdateRedColorDistortion (int value)
		{
			if (value > 0) {
				colorCorrectionComponents [0].enabled = true;
				colorCorrectionComponents [1].enabled = true;
				colorDistortionToggle.isOn = true;
			}
		}

		private void UpdateRandomEffects (int value)
		{
			if (value > 0) {
				randomizationComponent.enabled = true;
				randomizationToggle.isOn = true;
			}
		}

		private void FillComponents<T> (T[] componentArray)
		{
			componentArray [0] = stereoCameraLeftComponent.GetComponent<T> ();
			componentArray [1] = stereoCameraRightComponent.GetComponent<T> ();
		}
	}
}