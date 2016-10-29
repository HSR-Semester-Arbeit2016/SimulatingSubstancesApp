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

	public void ResetConfigurationScene ()
	{
		this.ResetPlayerPrefs ();
		this.ResetSliders ();
		this.ResetTextFields ();
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

	private void UpdateSliders() {
		Slider blurSlider = GameObject.Find ("BlurSlider").GetComponent<Slider> ();
		blurSlider.value = PlayerPrefs.GetFloat ("BlurLevel");
		Slider tunnelSlider = GameObject.Find ("TunnelSlider").GetComponent<Slider> ();
		tunnelSlider.value = PlayerPrefs.GetFloat ("TunnelLevel");
		Slider delaySlider = GameObject.Find ("DelaySlider").GetComponent<Slider> ();
		delaySlider.value = PlayerPrefs.GetFloat ("DelayLevel");

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
		PlayerPrefs.SetFloat ("DelayLevel", 0);
	}
}
