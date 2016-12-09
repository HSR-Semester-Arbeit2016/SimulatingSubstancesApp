using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadConfigurationViewModel : MonoBehaviour
{
    private static readonly List<string> defaultConfigurations = new List<string>(new[]
    {
        "Sober",
        "Slightly Drunk",
        "Drunk",
        "Very Drunk",
        "Create",
        "Delete"
    });

    private Configuration configuration;
    private Text errormessagesText;

    public Action<int> OnSelectionChanged;
    private ConfigFilesScrollList ConfigFilesList { get; set; }

    private void Start()
    {
        ConfigFilesList = GameObject.Find("GameController").GetComponent<ConfigFilesScrollList>();
        errormessagesText = GameObject.Find("ErrorMessagesText").GetComponent<Text>();
        configuration = new Configuration();
        ConfigFilesList.OnSelectionChanged = ShowSelectedConfigIndex;
    }

    public void LoadSelectedConfig()
    {
        LoadConfiguration(ConfigFilesList.SelectedConfig);
        SaveDataToPlayerPrefs(configuration);
        LoadCorrespondingScene(ConfigFilesList.SelectedConfig.FileName);
    }

    private void ShowSelectedConfigIndex(int index)
    {
        Debug.Log("Config index: " + index);
    }

    private void LoadConfiguration(ConfigFile selectedConfig)
    {
        try
        {
            Debug.Log("Selected Config Name: " + selectedConfig.FileName);
            if (defaultConfigurations.Contains(selectedConfig.FileName))
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
            ShowErrorMessage("Error at loading selectedConfig:", ex);
            Debug.Log("Error at loading selectedConfig");
        }
    }


    private void SaveDataToPlayerPrefs(Configuration selectedConfig)
    {
        Debug.Log("SaveDataToPlayerPrefs in LoadConfigurationViewModelCalled");
        PlayerPrefs.SetString("ConfigurationName", selectedConfig.Name);
        PlayerPrefs.SetFloat("BlurLevel", selectedConfig.BlurLevel);
        PlayerPrefs.SetFloat("TunnelLevel", selectedConfig.TunnelLevel);
        PlayerPrefs.SetInt("DelayLevel", selectedConfig.Delay);
        PlayerPrefs.SetInt("MotionBlur", selectedConfig.MotionBlur);
        PlayerPrefs.SetInt("RedColorDistortion", selectedConfig.RedColor);
        PlayerPrefs.SetInt("RandomEffects", selectedConfig.Randomization);
    }

    private void LoadDefaultConfig(string selectedConfigurationName)
    {
        switch (selectedConfigurationName)
        {
            case "Sober":
                LoadSoberConfig();
                break;
            case "Slightly Drunk":
                LoadSlightlyDrunkConfig();
                break;
            case "Drunk":
                LoadDrunkConfig();
                break;
            case "Very Drunk":
                LoadVeryDrunkConfig();
                break;
            case "Create":
            case "Delete":
                break;
            default:
                LoadSoberConfig();
                break;
        }
    }

    private void LoadSoberConfig()
    {
        configuration.Name = "Sober";
        configuration.BlurLevel = 0;
        configuration.TunnelLevel = 0;
        configuration.Delay = 0;
        configuration.MotionBlur = 0;
        configuration.RedColor = 0;
        configuration.Randomization = 0;
    }


    private void LoadSlightlyDrunkConfig()
    {
        configuration.Name = "Slightly Drunk";
        configuration.BlurLevel = 2;
        configuration.TunnelLevel = 0;
        configuration.Delay = 0;
        configuration.MotionBlur = 0;
        configuration.RedColor = 0;
        configuration.Randomization = 0;
    }

    private void LoadDrunkConfig()
    {
        configuration.Name = "Drunk";
        configuration.BlurLevel = 3;
        configuration.TunnelLevel = 0;
        configuration.Delay = 0;
        configuration.MotionBlur = 0;
        configuration.RedColor = 0;
        configuration.Randomization = 0;
    }

    private void LoadVeryDrunkConfig()
    {
        configuration.Name = "Very Drunk";
        configuration.BlurLevel = 4;
        configuration.TunnelLevel = 0;
        configuration.Delay = 0;
        configuration.MotionBlur = 0;
        configuration.RedColor = 0;
        configuration.Randomization = 0;
    }

    private void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    private void ShowErrorMessage(string message, Exception exception)
    {
        errormessagesText.text = message + "\n" + exception.Message;
    }

    private void LoadCorrespondingScene(string selectedConfigName)
    {
        switch (selectedConfigName)
        {
            case "Sober":
            case "Slightly Drunk":
            case "Drunk":
            case "Very Drunk":
                LoadScene(2);
                break;
            case "Create":
                LoadScene(1);
                break;
            case "Delete":
                LoadScene(3);
                break;
            default:
                LoadScene(2);
                break;
        }
    }
}