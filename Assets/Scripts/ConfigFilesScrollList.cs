using System;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.MetaData;
using UnityEngine;

namespace Assets.Scripts
{
    public class ConfigFilesScrollList : MonoBehaviour
    {
        private List<ConfigFile> configFilesList;
        public Transform contentPanel;
        public GameObject listButton;
        public Action<int> OnSelectionChanged;
        public ConfigFile SelectedConfig { get; private set; }

        public ConfigFile this[int index]
        {
            get { return configFilesList[index]; }
        }

        private void Start()
        {
            configFilesList = new List<ConfigFile>();
            FillListWithDefaultConfigs();
            FillListWithSavedConfigFiles();
            FillListInGui();
        }

        public void RemoveSelectedConfig()
        {
            foreach (var config in DefaultConfigurations.List)
            {
                if (config.Equals(SelectedConfig.FileName))
                {
                    throw new TriedToDeleteDefaultConfigException(Messages.ConfigDeletingDefaultError);
                }
            }
            configFilesList.Remove(SelectedConfig);
            ClearListInGui();
            FillListInGui();
        }

        private void FillListWithDefaultConfigs()
        {
            foreach (var fileName in DefaultConfigurations.List)
            {
                var newConfigFile = new ConfigFile(fileName, "");
                configFilesList.Add(newConfigFile);
            }
        }

        private void FillListWithSavedConfigFiles()
        {
            var fileEntries = Directory.GetFiles(Application.persistentDataPath);
            foreach (var fileName in fileEntries)
            {
                var newConfigFile = new ConfigFile(Path.GetFileName(fileName), fileName);
                configFilesList.Add(newConfigFile);
            }
        }

        private void FillListInGui()
        {
            foreach (var file in configFilesList)
            {
                var newButton = Instantiate(listButton);
                var button = newButton.GetComponent<ListButton>();
                button.nameLabel.text = file.FileName;
                var index = configFilesList.IndexOf(file);
                button.button.onClick.AddListener(() => { OnButtonClicked(index); });
                newButton.transform.SetParent(contentPanel);
            }
        }

        private void ClearListInGui()
        {
            contentPanel.DetachChildren();
        }

        private void OnButtonClicked(int index)
        {
            SelectedConfig = configFilesList[index];
            OnSelectionChanged(index);
        }
    }
}