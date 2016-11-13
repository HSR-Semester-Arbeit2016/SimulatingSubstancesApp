using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SaveConfigurationSceneViewModel : MonoBehaviour
{
	private ConfigurationDTO configuration;

	public SaveConfigurationSceneViewModel ()
	{
		this.configuration = new ConfigurationDTO ();
	}

	public void SaveToFile ()
	{
		this.SaveDataToConfigurationDTO ();
		this.SaveConfigurationDTOToFile ();
	}

	private void SaveDataToConfigurationDTO ()
	{
		InputField fileNameInputField = GameObject.Find ("FileNameInputField").GetComponent<InputField> ();
		Debug.Log ("Input field text: " + fileNameInputField.text); 
		this.configuration.Name = fileNameInputField.text;
		this.configuration.BlurLevel = PlayerPrefs.GetFloat ("BlurLevel");
		this.configuration.TunnelLevel = PlayerPrefs.GetFloat ("TunnelLevel");
		this.configuration.Delay = PlayerPrefs.GetInt ("DelayLevel");
		this.configuration.MotionBlur = PlayerPrefs.GetInt ("MotionBlur");
		this.configuration.RedColor = PlayerPrefs.GetInt ("RedColorDistortion");    
		this.configuration.Randomness = PlayerPrefs.GetInt ("RandomEffects");
	}


	private void SaveConfigurationDTOToFile ()
	{
		SaveConfiguration.Save (this.configuration);
	}


}
