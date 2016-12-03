using System;
using System.Collections;
using Assets.Scripts.MetaData;
using Assets.Scripts.MetaData.UI;
using UnityEngine;
using UnityEngine.UI;

public class SaveConfigurationSceneViewModel : MonoBehaviour
{
	private Text messagesText;

	public void SaveConfiguration ()
	{
        InputField fileNameInputField = GameObject.Find(ConfigurationControls.FileNameInput).GetComponent<InputField>();
#if DEBUG
        Debug.Log("Input field text: " + fileNameInputField.text);
#endif
		SaveToFile (GenerateConfiguration(fileNameInputField.text));
	}
    //TODO: Extract into ConfigurationFactory
	private Configuration GenerateConfiguration (string configName) {
	    return new Configuration
	    {
	        Name = configName,
	        BlurLevel = PlayerPrefs.GetFloat(PlayerPreferences.BlurLevel),
	        TunnelLevel = PlayerPrefs.GetFloat(PlayerPreferences.TunnelLevel),
	        Delay = PlayerPrefs.GetInt(PlayerPreferences.DelayLevel),
	        MotionBlur = PlayerPrefs.GetInt(PlayerPreferences.MotionBlur),
	        RedColor = PlayerPrefs.GetInt(PlayerPreferences.RedColorDistortion),
	        Randomization = PlayerPrefs.GetInt(PlayerPreferences.Randomization)
	    };
	}

	private void SaveToFile (Configuration configuration)
	{
		messagesText = GameObject.Find (ConfigurationControls.MessagesText).GetComponent<Text> ();
		try {
			FileManager.Save (configuration);
			messagesText.text = Messages.ConfigSaveSuccessful + configuration.Name;
		} catch (Exception ex) {
			this.ShowMessage (Messages.ConfigSaveError + configuration.Name, ex);
		}
	}

    //TODO: Possibly extract into HelperClass
	private void ShowMessage (string message, Exception ex)
	{
		messagesText.text = message + "\n" + ex.Message;
	}
}
