using System;
using System.Collections;
using Assets.Scripts.Helpers;
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
		SaveToFile (ConfigurationHelper.GenerateConfigurationByPlayerPrefs(fileNameInputField.text));
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
