using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	/// <summary>
	/// Class needed by the ListItemButton Prefab in order to keep the references to itself in the Unity Editor. 
	/// This class represents a buttom item of the configurations list shown in the Main Scene, like:
	///  ______________________________________________
	/// |                                              |
	/// |                Drunk                         |
	/// |______________________________________________|
	/// |                                              |
	/// |                Very Drunk                    |
	/// |______________________________________________|
	/// It contains the Button GUI component,the TextField GUI component which shows the configuration's name (Drunk),
	/// a reference to the list which self contains the button and a reference to the configurations file containing the 
	/// corresponding-to-configurations-name configuration values.
	/// This class is only used in the AddButtons() Method of the MainSceneViewModel when the ListItemButton prefabs are created
	/// and when the user selects an item from the list.
	/// </summary>
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

		/// <summary>
		/// This method calls the LoadSelectedConfiguration method of the MainSceneViewModel
		/// </summary>
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