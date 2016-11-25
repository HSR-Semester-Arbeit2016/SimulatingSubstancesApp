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
		this.SetBlurSize (PlayerPrefs.GetFloat ("BlurLevel"));
		this.SetTunnelValue (PlayerPrefs.GetFloat ("TunnelLevel"));
		this.UpdateSliders ();
		this.UpdateDropdowns ();
	}

	public void SetTunnelValue (float newTunnelValue)
	{
		PlayerPrefs.SetFloat ("TunnelLevel", newTunnelValue);
		this.SetEffectValueText (newTunnelValue, "TunnelLevelText");
		this.SetDrunkLevelText (newTunnelValue, "SelectedTunnelAlcoholLevelText");
	}


	public void SetBlurSize (float newBlurValue)
	{	
		PlayerPrefs.SetFloat ("BlurLevel", newBlurValue);
		this.SetEffectValueText (newBlurValue, "BlurLevelText");
		this.SetDrunkLevelText (newBlurValue, "SelectedBlurAlcoholLevelText");
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
		this.ResetPlayerPrefs ();
		this.ResetSliders ();
		this.ResetTextFields ();
		this.ResetDropdowns ();
	}

	private void SetEffectValueText (float value, String textFieldName)
	{
		Text blurValueText = GameObject.Find (textFieldName).GetComponent<Text> ();
		blurValueText.text = value.ToString ();
	}

	private void SetDrunkLevelText (float newValue, String textFieldName)
	{
		Text selectedAlcoholLevelText = GameObject.Find (textFieldName).GetComponent<Text> ();
		selectedAlcoholLevelText.text = newValue.ToString ();
		/*
		var myswitch = new Dictionary <Func<float,bool>, Action> { 
			{ x => x == 0 ,    () => selectedAlcoholLevelText.text = this.soberLevel }, 
			{ x => x < 4 ,    () => selectedAlcoholLevelText.text = this.soberLevel }, 
			{ x => x < 8 ,    () => selectedAlcoholLevelText.text = this.intoxicatedLevel },
			{ x => x < 12,    () => selectedAlcoholLevelText.text = this.drunkLevel },
			{ x => x < 15 ,   () => selectedAlcoholLevelText.text = this.veryDrunkLevel }  
		};
		myswitch.First (sw => sw.Key (newValue)).Value ();*/

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
		this.SetEffectValueText (0, "BlurLevelText");
		this.SetDrunkLevelText (0, "SelectedBlurAlcoholLevelText");
		this.SetEffectValueText (0, "TunnelLevelText");
		this.SetDrunkLevelText (0, "SelectedTunnelAlcoholLevelText");
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
