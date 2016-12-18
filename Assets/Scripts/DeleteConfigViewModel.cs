using System;
using Assets.Scripts.Helpers;
using Assets.Scripts.MetaData;
using Assets.Scripts.MetaData.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	/// <summary>
	/// Manages the deletion of an element in the Configurations List (corresponding GUI Element called ListPanel) of the DeleteConfiguration Scene. 
	/// </summary>
	public class DeleteConfigViewModel : MonoBehaviour
	{
		private Configuration configuration;
		/// <summary>
		/// Text GUI element used to show the configurations values to the user
		/// </summary>
		private Text messageText;

		public Action<int> OnSelectionChanged;

		/// <summary>
		/// Gets or sets the application path, which contains the path to a persistent data directory 
		/// </summary>
		/// <value>The application path.</value>
		public string ApplicationPath { get; set; }

		private ConfigFilesScrollList ConfigFilesScrollList { get; set; }

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
			} catch (Exception ex) {
				ShowErrorMessage (ex);
			}
		}

		private void ShowErrorMessage (Exception ex)
		{
			messageText.text = ex.Message;
		}
	}
}