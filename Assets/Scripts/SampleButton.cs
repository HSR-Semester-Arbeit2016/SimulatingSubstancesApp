using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class SampleButton : MonoBehaviour
	{
		public Button buttonComponent;
		private ConfigFile configurationFile;
		private MainSceneViewModel mainSceneViewModelConfigsList;
		public Text nameLabel;

		private void Start ()
		{
			buttonComponent.onClick.AddListener (OnButtonClick);
		}

		public void Setup (ConfigFile currentConfigurationFile, MainSceneViewModel currentMainSceneViewModelScrollList)
		{
			configurationFile = currentConfigurationFile;
			nameLabel.text = configurationFile.FileName;
			mainSceneViewModelConfigsList = currentMainSceneViewModelScrollList;
		}

		public void OnButtonClick ()
		{
			try {
				mainSceneViewModelConfigsList.LoadSelectedConfiguration (configurationFile);
			} catch (Exception ex) {
				#if DEBUG
				Debug.Log ("Exception at OnButtonClick at SampleButton:" + ex.Message);
				#endif
			}
		}
	}
}