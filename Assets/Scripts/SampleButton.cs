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

        private void Start()
        {
            buttonComponent.onClick.AddListener(OnButtonClick);
        }

        public void Setup(ConfigFile currentConfigurationFile, MainSceneViewModel currentScrollList)
        {
            configurationFile = currentConfigurationFile;
            nameLabel.text = configurationFile.FileName;
            mainSceneViewModelConfigsList = currentScrollList;
        }

        public void OnButtonClick()
        {
            mainSceneViewModelConfigsList.LoadSelectedConfiguration(configurationFile);
            Debug.Log("Button: " + configurationFile.FileName + "clicked!");
        }
    }
}