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
			// TODO look fr dlete scene GameObject.Find("").scene
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
#if DEBUG
				Debug.Log ("Button: " + configurationFile.FileName + "clicked!");
#endif
			} catch (Exception) {
				
			}
		}
	}
}