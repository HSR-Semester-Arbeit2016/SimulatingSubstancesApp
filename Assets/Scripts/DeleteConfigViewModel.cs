using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteConfigViewModel : MonoBehaviour
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
    private Text messageText;
    public Action<int> OnSelectionChanged;
    public string ApplicationPath { get; set; }
    public ConfigFilesScrollList ConfigFilesScrollList { get; set; }

    private void Start()
    {
        ApplicationPath = Application.persistentDataPath;
        ConfigFilesScrollList = GameObject.Find("GameController").GetComponent<ConfigFilesScrollList>();
        messageText = GameObject.Find("MessagesText").GetComponent<Text>();
        configuration = new Configuration();
        ConfigFilesScrollList.OnSelectionChanged = ShowSelectedConfig;
    }


    private void ShowSelectedConfig(int index)
    {
        Debug.Log("ShowSelectedConfig called with index: " + index);
        try
        {
            ResetMessages("");
            var selectedConfig = ConfigFilesScrollList[index];
            if (defaultConfigurations.Contains(selectedConfig.FileName))
            {
                LoadDefaultConfig(selectedConfig.FileName);
                Debug.Log("ShowSelectedConfig called with file name: " + selectedConfig.FileName);
                messageText.text += selectedConfig.ToString();
            }
            else
            {
                configuration = FileManager.Load(selectedConfig.FileName);
                messageText.text += selectedConfig.ToString();
            }
        }
        catch (Exception ex)
        {
            ShowErrorMessage("Error at loading configuration", ex);
        }
    }

    public void Delete()
    {
        try
        {
            ResetMessages("");
            var selectedConfig = ConfigFilesScrollList.SelectedConfig;
            ConfigFilesScrollList.RemoveSelectedConfig();
            FileManager.DeleteFile(selectedConfig.FileName);
        }
        catch (TriedToDeleteDefaultConfigException ex)
        {
            ShowErrorMessage("Error at deleting configuration:", ex);
        }
        catch (Exception ex)
        {
            ShowErrorMessage("Error at deleting configuration:", ex);
        }
    }

    private void ShowErrorMessage(string message, Exception ex)
    {
        messageText.text = message + "\n" + ex.Message;
    }

    private void ResetMessages(string value)
    {
        configuration.Name = value;
        configuration.BlurLevel = 0;
        configuration.TunnelLevel = 0;
        configuration.Delay = 0;
        configuration.MotionBlur = 0;
        configuration.RedColor = 0;
        configuration.Randomization = 0;
    }

    private void LoadDefaultConfig(string value)
    {
        switch (value)
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
                ResetMessages(value);
                break;
            case "Delete":
                ResetMessages(value);
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
}