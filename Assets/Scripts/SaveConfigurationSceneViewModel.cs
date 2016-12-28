using System;
using Assets.Scripts.DTO;
using Assets.Scripts.Helpers;
using Assets.Scripts.MetaData;
using Assets.Scripts.MetaData.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	/// <summary>
	///The management of the Configuration Scene is divided between the ConfigurationViewModel class and the SaveConfigurationSceneViewModel.
	///The SaveConfigurationSceneViewModel manages the saving of the configuration chosen by the user to a file
	/// </summary>
	public class SaveConfigurationSceneViewModel : MonoBehaviour
	{
		/// <summary>
		/// Text field in GUI for messaging
		/// </summary>
		private Text messagesText;

		public void SaveConfiguration ()
		{
			var fileNameInputField = GameObject.Find (ConfigurationControls.FileNameInput).GetComponent<InputField> ();
			SaveToFile (ConfigurationHelper.GenerateConfigurationByPlayerPrefs (fileNameInputField.text));
		}

		private void SaveToFile (Configuration configuration)
		{
			messagesText = GameObject.Find (ConfigurationControls.MessagesText).GetComponent<Text> ();
			try {
				FileManager.Save (configuration);
				messagesText.text = Messages.ConfigSaveSuccessful + configuration.Name;
			} catch (Exception ex) {
				ShowMessage (Messages.ConfigSaveError + configuration.Name, ex);
			}
		}

		private void ShowMessage (string message, Exception ex)
		{
			messagesText.text = message + "\n" + ex.Message;
		}
	}
}