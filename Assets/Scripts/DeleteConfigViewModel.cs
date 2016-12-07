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

        public string ApplicationPath { get; set; }

        public ConfigFilesScrollList ConfigFilesScrollList { get; set; }

        private void Start()
        {
            ApplicationPath = Application.persistentDataPath;
            ConfigFilesScrollList =
                GameObject.Find(DeleteConfigurationControls.GameController).GetComponent<ConfigFilesScrollList>();
            messageText = GameObject.Find(DeleteConfigurationControls.ErrorMessagesText).GetComponent<Text>();
            configuration = new Configuration();
            ConfigFilesScrollList.OnSelectionChanged = ShowSelectedConfig;
        }


        private void ShowSelectedConfig(int index)
        {
            Debug.Log("ShowSelectedConfig called with index: " + index);
            try
            {
                ConfigurationHelper.ResetConfigEffectValues(configuration);
                var selectedConfig = ConfigFilesScrollList[index];
                if (DefaultConfigurations.List.Contains(selectedConfig.FileName))
                {
                    switch (selectedConfig.FileName)
                    {
                        case ConfigurationNames.CreateNew:
                        case ConfigurationNames.DeleteExisting:
                            ConfigurationHelper.ResetConfigEffectValues(configuration);
                            break;
                        default:
                            configuration = ConfigurationHelper.GetDefaultConfig(selectedConfig.FileName);
                            break;
                    }
                    Debug.Log("ShowSelectedConfig called with file name: " + selectedConfig.FileName);
                    messageText.text = configuration.ToString();
                }
                else
                {
                    configuration = FileManager.Load(selectedConfig.FileName);
                    messageText.text = configuration.ToString();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(Messages.ConfigLoadingError, ex);
            }
        }

        public void Delete()
        {
            try
            {
                ConfigurationHelper.ResetConfigEffectValues(configuration);
                var selectedConfig = ConfigFilesScrollList.SelectedConfig;
                ConfigFilesScrollList.RemoveSelectedConfig();
                FileManager.DeleteFile(selectedConfig.FileName);
            }
            catch (TriedToDeleteDefaultConfigException ex)
            {
                ShowErrorMessage(Messages.ConfigDeletingError, ex);
            }
            catch (Exception ex)
            {
                //TODO: Make sure the custom-exception is actually needed and not just a different MessageText
                ShowErrorMessage(Messages.ConfigDeletingError, ex);
            }
        }

        //TODO: Possibly extract into HelperClass
        private void ShowErrorMessage(string message, Exception ex)
        {
            messageText.text = message + "\n" + ex.Message;
        }
    }
}