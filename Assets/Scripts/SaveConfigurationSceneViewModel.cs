using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SaveConfigurationSceneViewModel : MonoBehaviour
{
	private Configuration configuration;

	private Text messagesText;

	public SaveConfigurationSceneViewModel ()
	{
		this.configuration = new Configuration ();
	}

	public void SaveConfiguration ()
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
		this.configuration.Randomization = PlayerPrefs.GetInt ("RandomEffects");
	}


	private void SaveConfigurationDTOToFile ()
	{
		messagesText = GameObject.Find ("MessagesText").GetComponent<Text> ();
		try {
			FileManager.Save (this.configuration);
			messagesText.text = configuration.Name + " Saved!";
		} catch (Exception ex) {
			this.ShowMessage ("Error at saving: " + configuration.Name, ex);
		}
	}


	private void ShowMessage (string message, Exception ex)
	{
		messagesText.text = message + "\n" + ex.Message;
	}
}
