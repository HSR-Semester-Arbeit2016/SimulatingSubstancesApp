using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityStandardAssets.ImageEffects;
using System.Collections.Generic;
using System.Linq;

public class ConfigurationViewModel : MonoBehaviour
{
	
	void Start ()
	{
		SetBlurSize (PlayerPrefs.GetFloat ("BlurLevel"));
		SetTunnelValue (PlayerPrefs.GetFloat ("TunnelLevel"));
		UpdateSliders ();
		UpdateDropdowns ();
	}

	public void SetTunnelValue (float newTunnelValue)
	{
		PlayerPrefs.SetFloat ("TunnelLevel", newTunnelValue);
		SetEffectValueText (newTunnelValue, "TunnelLevelText");
	}


	public void SetBlurSize (float newBlurValue)
	{	
		PlayerPrefs.SetFloat ("BlurLevel", newBlurValue);
		SetEffectValueText (newBlurValue, "BlurLevelText");
	}

	public void SetDelay (int value)
	{
		PlayerPrefs.SetInt ("DelayLevel", value);
	}

	public void SetMotionBlur (int value)
	{
		PlayerPrefs.SetInt ("MotionBlur", value);
	}

	/// <summary>
	/// Sets the red color distortion.
	/// </summary>
	/// <param name="redColorDistortion">Red color distortion.</param>
	public void SetRedColorDistortion (int value)
	{
		PlayerPrefs.SetInt ("RedColorDistortion", value);
	}

	/// <summary>
	/// Sets the random effects on or off. 
	/// </summary>
	/// <param name="randomEffect">Random effect.</param>
	public void SetRandomEffects (int value)
	{
		PlayerPrefs.SetInt ("RandomEffects", value);
	}

	public void Reset ()
	{
		ResetPlayerPrefs ();
		ResetSliders ();
		ResetTextFields ();
		ResetDropdowns ();
	}

	private void SetEffectValueText (float value, String textFieldName)
	{
		Text blurValueText = GameObject.Find (textFieldName).GetComponent<Text> ();
		blurValueText.text = value.ToString ();
	}

	private void UpdateSliders ()
	{
		UpdateSlider ("BlurSlider", "BlurLevel");
		UpdateSlider ("TunnelSlider", "TunnelLevel");
	}

	private void UpdateSlider (string sliderName, string playerPrefName)
	{
		Slider slider = GameObject.Find (sliderName).GetComponent<Slider> ();
		slider.value = PlayerPrefs.GetFloat (playerPrefName);
	}

	private void UpdateDropdowns ()
	{
		UpdateDropdown ("DelayDropdown", "DelayLevel");
		UpdateDropdown ("ColorDropdown", "RedColorDistortion");
		UpdateDropdown ("MotionBlurDropdown", "MotionBlur");
		UpdateDropdown ("RandomDropdown", "RandomEffects");
	}

	private void UpdateDropdown (string dropdownName, string playerPrefName)
	{
		Dropdown dropdown = GameObject.Find (dropdownName).GetComponent<Dropdown> ();
		dropdown.value = PlayerPrefs.GetInt (playerPrefName);
	}

	private void ResetSliders ()
	{
		Slider[] configurationSceneSliders = GameObject.FindObjectsOfType<Slider> ();
		foreach (Slider slider in configurationSceneSliders) {
			slider.value = 0;
		}
	}

	private void ResetTextFields ()
	{
		SetEffectValueText (0, "BlurLevelText");
		SetEffectValueText (0, "TunnelLevelText");
	}

	private void ResetPlayerPrefs ()
	{
		PlayerPrefs.SetFloat ("BlurLevel", 0);
		PlayerPrefs.SetFloat ("TunnelLevel", 0);
		PlayerPrefs.SetInt ("DelayLevel", 0);
		PlayerPrefs.SetInt ("MotionBlur", 0);
		PlayerPrefs.SetInt ("RedColorDistortion", 0);    
		PlayerPrefs.SetInt ("RandomEffects", 0);
	}

	private void ResetDropdowns ()
	{
		Dropdown[] configurationSceneDropdowns = GameObject.FindObjectsOfType<Dropdown> ();
		foreach (Dropdown dropdown in configurationSceneDropdowns) {
			dropdown.value = 0;
		}
	}
}
