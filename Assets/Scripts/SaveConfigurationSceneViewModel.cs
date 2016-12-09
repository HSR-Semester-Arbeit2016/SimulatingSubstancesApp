using System;
using UnityEngine;
using UnityEngine.UI;

public class SaveConfigurationSceneViewModel : MonoBehaviour
{
    private Text messagesText;

    public void SaveConfiguration()
    {
        var fileNameInputField = GameObject.Find("FileNameInputField").GetComponent<InputField>();
        Debug.Log("Input field text: " + fileNameInputField.text);
        SaveToFile(GenerateConfiguration(fileNameInputField.text));
    }

    private Configuration GenerateConfiguration(string configName)
    {
        return new Configuration
        {
            Name = configName,
            BlurLevel = PlayerPrefs.GetFloat("BlurLevel"),
            TunnelLevel = PlayerPrefs.GetFloat("TunnelLevel"),
            Delay = PlayerPrefs.GetInt("DelayLevel"),
            MotionBlur = PlayerPrefs.GetInt("MotionBlur"),
            RedColor = PlayerPrefs.GetInt("RedColorDistortion"),
            Randomization = PlayerPrefs.GetInt("RandomEffects")
        };
    }


    private void SaveToFile(Configuration configuration)
    {
        messagesText = GameObject.Find("MessagesText").GetComponent<Text>();
        try
        {
            FileManager.Save(configuration);
            messagesText.text = configuration.Name + " Saved!";
        }
        catch (Exception ex)
        {
            ShowMessage("Error at saving: " + configuration.Name, ex);
        }
    }


    private void ShowMessage(string message, Exception ex)
    {
        messagesText.text = message + "\n" + ex.Message;
    }
}