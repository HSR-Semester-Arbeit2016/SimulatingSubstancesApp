using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadDefaultConfigViewModel : MonoBehaviour
{

	public void ApplySelectedConfig ()
	{
		Dropdown defaultConfigDropdown = GameObject.Find ("DefaultConfigDropdown").GetComponent<Dropdown> ();
		this.LoadDefaultConfig (defaultConfigDropdown.value);	

	}

	private void LoadDefaultConfig (int value)
	{
		switch (value) {
		case 0:
			this.LoadSoberConfig ();
			break;    
		case 1:
			this.LoadSlightlyDrunkConfig ();
			break;
		case 2:
			this.LoadDrunkConfig ();
			break;
		case 3:
			this.LoadVeryDrunkConfig ();
			break;
		default:
			this.LoadSoberConfig ();
			break;
		}
	}

	public void OnDropdownValueChanged (int value)
	{
		switch (value) {
		case 0:
			this.ShowSelectedConfigValues (0.0f, 0.0f, 0, 0, 0, 0);
			break;
		case 1:
			this.ShowSelectedConfigValues (2.0f, 0.0f, 0, 0, 0, 0);
			break;
		case 2:
			this.ShowSelectedConfigValues (3.0f, 0.0f, 0, 0, 0, 0);
			break;
		case 3:
			this.ShowSelectedConfigValues (4.0f, 0.0f, 0, 0, 0, 0);
			break;
		default:
			this.ShowSelectedConfigValues (0.0f, 0.0f, 0, 0, 0, 0);
			break;
		}
	}

	private void LoadSoberConfig ()
	{
		PlayerPrefs.SetFloat ("BlurLevel", 0);
		PlayerPrefs.SetFloat ("TunnelLevel", 0);
		PlayerPrefs.SetInt ("DelayLevel", 0);
		PlayerPrefs.SetInt ("MotionBlur", 0);
		PlayerPrefs.SetInt ("RedColorDistortion", 0);    
		PlayerPrefs.SetInt ("RandomEffects", 0);
		this.ShowSelectedConfigValues (0.0f, 0.0f, 0, 0, 0, 0);
	}


	private void LoadSlightlyDrunkConfig ()
	{
		PlayerPrefs.SetFloat ("BlurLevel", 2);
		PlayerPrefs.SetFloat ("TunnelLevel", 0);
		PlayerPrefs.SetInt ("DelayLevel", 0);
		PlayerPrefs.SetInt ("MotionBlur", 0);
		PlayerPrefs.SetInt ("RedColorDistortion", 0);    
		PlayerPrefs.SetInt ("RandomEffects", 0);
		this.ShowSelectedConfigValues (2.0f, 0.0f, 0, 0, 0, 0);
	}

	private void LoadDrunkConfig ()
	{
		PlayerPrefs.SetFloat ("BlurLevel", 3);
		PlayerPrefs.SetFloat ("TunnelLevel", 0);
		PlayerPrefs.SetInt ("DelayLevel", 0);
		PlayerPrefs.SetInt ("MotionBlur", 0);
		PlayerPrefs.SetInt ("RedColorDistortion", 0);    
		PlayerPrefs.SetInt ("RandomEffects", 0);
		this.ShowSelectedConfigValues (3.0f, 0.0f, 0, 0, 0, 0);
	}

	private void LoadVeryDrunkConfig ()
	{
		PlayerPrefs.SetFloat ("BlurLevel", 4);
		PlayerPrefs.SetFloat ("TunnelLevel", 0);
		PlayerPrefs.SetInt ("DelayLevel", 0);
		PlayerPrefs.SetInt ("MotionBlur", 0);
		PlayerPrefs.SetInt ("RedColorDistortion", 0);    
		PlayerPrefs.SetInt ("RandomEffects", 0);
		this.ShowSelectedConfigValues (4.0f, 0.0f, 0, 0, 0, 0);
	}


	private void ShowSelectedConfigValues (float blurLevel, float tunnelLevel, int delayLevel, int motionBlur, int redColor, int random)
	{
		Text configurationValuesText = GameObject.Find ("ConfigurationValuesText").GetComponent<Text> ();
		configurationValuesText.text = "Blur Level: " + blurLevel.ToString ();
		configurationValuesText.text += "\nTunnel Level: " + tunnelLevel.ToString ();
		configurationValuesText.text += "\nDelay Level: " + delayLevel.ToString ();
		configurationValuesText.text += "\nMotion Blur: " + (motionBlur == 0 ? "Off" : "On");  
		configurationValuesText.text += "\nRedColor Distortion: " + (redColor == 0 ? "Off" : "On");
		configurationValuesText.text += "\nRandom Effects: " + (random == 0 ? "Off" : "On");

	}
}
