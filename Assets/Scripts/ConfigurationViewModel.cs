using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityStandardAssets.ImageEffects;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.MetaData;
using Assets.Scripts.MetaData.UI;

public class ConfigurationViewModel : MonoBehaviour
{
	
	void Start ()
	{
		SetBlurSize (PlayerPrefs.GetFloat (PlayerPreferences.BlurLevel));
		SetTunnelValue (PlayerPrefs.GetFloat (PlayerPreferences.TunnelLevel));
		UpdateSliders ();
		UpdateDropdowns ();
	}

	public void SetTunnelValue (float newTunnelValue)
	{
		PlayerPrefs.SetFloat (PlayerPreferences.TunnelLevel, newTunnelValue);
		SetEffectValueText (newTunnelValue, ConfigurationControls.TunnelLevelText);
	}


	public void SetBlurSize (float newBlurValue)
	{	
		PlayerPrefs.SetFloat (PlayerPreferences.BlurLevel, newBlurValue);
		SetEffectValueText (newBlurValue, ConfigurationControls.BlurLevelText);
	}

	public void SetDelay (int value)
	{
		PlayerPrefs.SetInt (PlayerPreferences.DelayLevel, value);
	}

	public void SetMotionBlur (int value)
	{
		PlayerPrefs.SetInt (PlayerPreferences.MotionBlur, value);
	}

	/// <summary>
	/// Sets the red color distortion on or off.
	/// </summary>
	/// <param name="value">Red color distortion.</param>
	public void SetRedColorDistortion (int value)
	{
		PlayerPrefs.SetInt (PlayerPreferences.RedColorDistortion, value);
	}

	/// <summary>
	/// Sets the effect randomization on or off. 
	/// </summary>
	/// <param name="value">Randomization value.</param>
	public void SetRandomEffects (int value)
	{
		PlayerPrefs.SetInt (PlayerPreferences.Randomization, value);
	}

	public void Reset ()
	{
		ResetPlayerPrefs ();
		ResetSliders ();
		ResetTextFields ();
		ResetDropdowns ();
	}

    //TODO: Possibly extract into UI-HelperClass
    private void SetEffectValueText (float value, String textFieldName)
	{
		Text blurValueText = GameObject.Find (textFieldName).GetComponent<Text> ();
		blurValueText.text = value.ToString ();
	}

	private void UpdateSliders ()
	{
		UpdateSlider (ConfigurationControls.BlurSlider, PlayerPreferences.BlurLevel);
		UpdateSlider (ConfigurationControls.TunnelSlider, PlayerPreferences.TunnelLevel);
	}

    //TODO: Possibly extract into UI-HelperClass
    private void UpdateSlider (string sliderName, string playerPrefName)
	{
		Slider slider = GameObject.Find (sliderName).GetComponent<Slider> ();
		slider.value = PlayerPrefs.GetFloat (playerPrefName);
	}

	private void UpdateDropdowns ()
	{
		UpdateDropdown (ConfigurationControls.DelayDropdown, PlayerPreferences.DelayLevel);
		UpdateDropdown (ConfigurationControls.ColorDropdown, PlayerPreferences.RedColorDistortion);
		UpdateDropdown (ConfigurationControls.MotionBlurDropdown, PlayerPreferences.MotionBlur);
		UpdateDropdown (ConfigurationControls.RandomizationDropdown, PlayerPreferences.Randomization);
	}

    //TODO: Possibly extract into UI-HelperClass
    private void UpdateDropdown (string dropdownName, string playerPrefName)
	{
		Dropdown dropdown = GameObject.Find (dropdownName).GetComponent<Dropdown> ();
		dropdown.value = PlayerPrefs.GetInt (playerPrefName);
	}

    //TODO: Possibly extract into UI-HelperClass
    private void ResetSliders ()
	{
		Slider[] configurationSceneSliders = GameObject.FindObjectsOfType<Slider> ();
		foreach (Slider slider in configurationSceneSliders) {
			slider.value = 0;
		}
	}

    //TODO: Possibly extract into UI-HelperClass
    private void ResetTextFields ()
	{
		SetEffectValueText (0, ConfigurationControls.BlurLevelText);
		SetEffectValueText (0, ConfigurationControls.TunnelLevelText);
	}

    //TODO: Possibly extract into HelperClass
	private void ResetPlayerPrefs ()
	{
		PlayerPrefs.SetFloat (PlayerPreferences.BlurLevel, 0);
		PlayerPrefs.SetFloat (PlayerPreferences.TunnelLevel, 0);
		PlayerPrefs.SetInt (PlayerPreferences.DelayLevel, 0);
		PlayerPrefs.SetInt (PlayerPreferences.MotionBlur, 0);
		PlayerPrefs.SetInt (PlayerPreferences.RedColorDistortion, 0);    
		PlayerPrefs.SetInt (PlayerPreferences.Randomization, 0);
	}

    //TODO: Possibly extract into UI-HelperClass
	private void ResetDropdowns ()
	{
		Dropdown[] configurationSceneDropdowns = GameObject.FindObjectsOfType<Dropdown> ();
		foreach (Dropdown dropdown in configurationSceneDropdowns) {
			dropdown.value = 0;
		}
	}
}
