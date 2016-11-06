using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityStandardAssets.ImageEffects;
using System.Collections.Generic;
using System.Linq;

public class ConfigurationViewModel : MonoBehaviour
{
	private readonly String soberLevel = "Sober";
	private readonly String intoxicatedLevel = "Slightly intoxicated";
	private readonly String drunkLevel = "Drunk";
	private readonly String veryDrunkLevel = "Very drunk";

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
		this.SetDrunkLevelText (newBlurValue * 3, "SelectedBlurAlcoholLevelText");
	}


	public void SetDelay (int delay)
	{
		PlayerPrefs.SetInt ("DelayLevel", delay);
	}

	/// <summary>
	/// Sets the red color distorsion.
	/// </summary>
	/// <param name="redColorDistorsion">Red color distorsion.</param>
	public void SetRedColorDistorsion (int redColorDistorsion)
	{
		PlayerPrefs.SetInt ("RedColorDistorsion", redColorDistorsion);
	}

	/// <summary>
	/// Sets the random effects on or off. 
	/// </summary>
	/// <param name="randomEffect">Random effect.</param>
	public void SetRandomEffects (int randomEffect)
	{
		PlayerPrefs.SetInt ("RandomEffects", randomEffect);
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

	private void SetDrunkLevelText (float newValue, String textFiedlName)
	{
		Text selectedAlcoholLevelText = GameObject.Find (textFiedlName).GetComponent<Text> ();
		var myswitch = new Dictionary <Func<float,bool>, Action> { 
			{ x => x == 0 ,    () => selectedAlcoholLevelText.text = this.soberLevel }, 
			{ x => x < 4 ,    () => selectedAlcoholLevelText.text = this.soberLevel }, 
			{ x => x < 8 ,    () => selectedAlcoholLevelText.text = this.intoxicatedLevel },
			{ x => x < 12,    () => selectedAlcoholLevelText.text = this.drunkLevel },
			{ x => x < 15 ,   () => selectedAlcoholLevelText.text = this.veryDrunkLevel }  
		};
		myswitch.First (sw => sw.Key (newValue)).Value ();

	}

	private void UpdateSliders ()
	{
		Slider blurSlider = GameObject.Find ("BlurSlider").GetComponent<Slider> ();
		blurSlider.value = PlayerPrefs.GetFloat ("BlurLevel");
		Slider tunnelSlider = GameObject.Find ("TunnelSlider").GetComponent<Slider> ();
		tunnelSlider.value = PlayerPrefs.GetFloat ("TunnelLevel");
	}

	private void UpdateDropdowns ()
	{
		Dropdown delayDropdown = GameObject.Find ("DelayDropdown").GetComponent<Dropdown> ();
		delayDropdown.value = PlayerPrefs.GetInt ("DelayLevel");
		Dropdown colorDropdown = GameObject.Find ("ColorDropdown").GetComponent<Dropdown> ();
		colorDropdown.value = PlayerPrefs.GetInt ("RedColorDistorsion");
		Dropdown randomDropdown = GameObject.Find ("RandomDropdown").GetComponent<Dropdown> ();
		randomDropdown.value = PlayerPrefs.GetInt ("RandomEffects");
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
		PlayerPrefs.SetInt ("RedColorDistorsion", 0);
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
