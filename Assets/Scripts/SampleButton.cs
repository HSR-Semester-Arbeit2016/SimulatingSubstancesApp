using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SampleButton : MonoBehaviour
{

	public Button buttonComponent;
	public Text nameLabel;
	private MainSceneViewModel mainSceneViewModelConfigsList;
	private ConfigFile configurationFile;

	void Start ()
	{
		buttonComponent.onClick.AddListener (OnButtonClick);
	}

	public void Setup (ConfigFile currentConfigurationFile, MainSceneViewModel currentScrollList)
	{
		configurationFile = currentConfigurationFile;
		nameLabel.text = configurationFile.FileName;
		mainSceneViewModelConfigsList = currentScrollList;
	}

	public void OnButtonClick ()
	{
		mainSceneViewModelConfigsList.LoadSelectedConfiguration (configurationFile);
		#if DEBUG
		Debug.Log ("Button: " + configurationFile.FileName + "clicked!");
		#endif
	}
}