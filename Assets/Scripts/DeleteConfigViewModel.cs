using System;
using Assets.Scripts.Helpers;
using Assets.Scripts.MetaData;
using Assets.Scripts.MetaData.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class DeleteConfigViewModel : MonoBehaviour
	{
		private Configuration configuration;
		private Text messageText;

		public Action<int> OnSelectionChanged;

		/// <summary>
		/// Gets or sets the application path, which contains the path to a persistent data directory 
		/// </summary>
		/// <value>The application path.</value>
		public string ApplicationPath { get; set; }

		public ConfigFilesScrollList ConfigFilesScrollList { get; set; }

		private void Start ()
		{
			ApplicationPath = Application.persistentDataPath;
			ConfigFilesScrollList =
                GameObject.Find (DeleteConfigurationControls.GameController).GetComponent<ConfigFilesScrollList> ();
			messageText = GameObject.Find (DeleteConfigurationControls.ErrorMessagesText).GetComponent<Text> ();
			configuration = new Configuration ();
			ConfigFilesScrollList.OnSelectionChanged = ShowSelectedConfig;
		}


		private void ShowSelectedConfig (int index)
		{
			try {
				ConfigurationHelper.ResetConfigEffectValues (configuration);
				var selectedConfig = ConfigFilesScrollList [index];
				configuration = FileManager.Load (selectedConfig.FileName);
				messageText.text = configuration.ToString ();

			} catch (Exception ex) {
				ShowErrorMessage (ex);
			}
		}

		public void Delete ()
		{
			try {
				ConfigurationHelper.ResetConfigEffectValues (configuration);
				var selectedConfig = ConfigFilesScrollList.SelectedConfig;
				ConfigFilesScrollList.RemoveSelectedConfig ();
				FileManager.DeleteFile (selectedConfig.FileName);
			} catch (TriedToDeleteDefaultConfigException ex) {
				ShowErrorMessage (ex);
			} catch (Exception ex) {
				ShowErrorMessage (ex);
			}
		}

		//TODO: Possibly extract into HelperClass
		private void ShowErrorMessage (Exception ex)
		{
			messageText.text = ex.Message;
		}
	}
}