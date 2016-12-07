using System;
using Assets.Scripts.Helpers;
using Assets.Scripts.MetaData;
using Assets.Scripts.MetaData.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LoadConfigurationViewModel : MonoBehaviour
    {
        private ConfigFilesScrollList configFilesList;
        private Configuration configuration;
        private Text errormessagesText;
        public Action<int> OnSelectionChanged;

        private void Start()
        {
            configFilesList =
                GameObject.Find(LoadConfigurationControls.GameController).GetComponent<ConfigFilesScrollList>();
            errormessagesText = GameObject.Find(LoadConfigurationControls.ErrorMessagesText).GetComponent<Text>();
            configuration = new Configuration();
            configFilesList.OnSelectionChanged = ShowSelectedConfigIndex;
        }

        public void LoadSelectedConfig()
        {
            LoadConfigurationFromConfig(configFilesList.SelectedConfig);
            SaveDataToPlayerPrefs(configuration);
            LoadCorrespondingScene(configFilesList.SelectedConfig.FileName);
        }

        private void ShowSelectedConfigIndex(int index)
        {
            Debug.Log("Config index: " + index);
        }

        private void LoadConfigurationFromConfig(ConfigFile selectedConfig)
        {
            try
            {
                Debug.Log("Selected Config Name: " + selectedConfig.FileName);
                if (DefaultConfigurations.List.Contains(selectedConfig.FileName))
                {
                    LoadDefaultConfig(selectedConfig.FileName);
                }
                else
                {
                    configuration = FileManager.Load(selectedConfig.FileName);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(Messages.ConfigLoadingError, ex);
                Debug.Log("Error at loading configuration");
            }
        }


        private void SaveDataToPlayerPrefs(Configuration selectedConfig)
        {
            Debug.Log("SaveDataToPlayerPrefs in LoadConfigurationViewModelCalled");
            PlayerPrefs.SetString(PlayerPreferences.ConfigurationName, configuration.Name);
            PlayerPrefs.SetFloat(PlayerPreferences.BlurLevel, configuration.BlurLevel);
            PlayerPrefs.SetFloat(PlayerPreferences.TunnelLevel, configuration.TunnelLevel);
            PlayerPrefs.SetInt(PlayerPreferences.DelayLevel, configuration.Delay);
            PlayerPrefs.SetInt(PlayerPreferences.MotionBlur, configuration.MotionBlur);
            PlayerPrefs.SetInt(PlayerPreferences.RedColorDistortion, configuration.RedColor);
            PlayerPrefs.SetInt(PlayerPreferences.Randomization, configuration.Randomization);
        }

        private void LoadDefaultConfig(string configName)
        {
            switch (configName)
            {
                case ConfigurationNames.CreateNew:
                case ConfigurationNames.DeleteExisting:
                    break;
                default:
                    configuration = ConfigurationHelper.GetDefaultConfig(configName);
                    break;
            }
        }

        private void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        //TODO: Possibly extract into HelperClass
        private void ShowErrorMessage(string message, Exception exception)
        {
            errormessagesText.text = message + "\n" + exception.Message;
        }

        private void LoadCorrespondingScene(string selectedConfig)
        {
            switch (selectedConfig)
            {
                case ConfigurationNames.Sober:
                case ConfigurationNames.SlightlyDrunk:
                case ConfigurationNames.Drunk:
                case ConfigurationNames.VeryDrunk:
                    LoadScene(2);
                    break;
                case ConfigurationNames.CreateNew:
                    LoadScene(1);
                    break;
                case ConfigurationNames.DeleteExisting:
                    LoadScene(3);
                    break;
                default:
                    LoadScene(2);
                    break;
            }
        }
    }
}