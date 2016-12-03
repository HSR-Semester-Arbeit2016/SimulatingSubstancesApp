using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityStandardAssets.ImageEffects;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Helpers;
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
		UiHelper.SetEffectValueText (newTunnelValue, ConfigurationControls.TunnelLevelText);
	}


	public void SetBlurSize (float newBlurValue)
	{	
		PlayerPrefs.SetFloat (PlayerPreferences.BlurLevel, newBlurValue);
		UiHelper.SetEffectValueText (newBlurValue, ConfigurationControls.BlurLevelText);
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
		PlayerPrefHelper.ResetPlayerPrefs ();
		UiHelper.ResetSliders ();
		ResetTextFields ();
		UiHelper.ResetDropdowns ();
	}

	private void UpdateSliders ()
	{
		UiHelper.UpdateSlider (ConfigurationControls.BlurSlider, PlayerPreferences.BlurLevel);
		UiHelper.UpdateSlider (ConfigurationControls.TunnelSlider, PlayerPreferences.TunnelLevel);
	}

	private void UpdateDropdowns ()
	{
		UiHelper.UpdateDropdown (ConfigurationControls.DelayDropdown, PlayerPreferences.DelayLevel);
        UiHelper.UpdateDropdown (ConfigurationControls.ColorDropdown, PlayerPreferences.RedColorDistortion);
        UiHelper.UpdateDropdown (ConfigurationControls.MotionBlurDropdown, PlayerPreferences.MotionBlur);
        UiHelper.UpdateDropdown (ConfigurationControls.RandomizationDropdown, PlayerPreferences.Randomization);
	}

    private void ResetTextFields ()
	{
		UiHelper.SetEffectValueText(0, ConfigurationControls.BlurLevelText);
		UiHelper.SetEffectValueText(0, ConfigurationControls.TunnelLevelText);
	}
}
