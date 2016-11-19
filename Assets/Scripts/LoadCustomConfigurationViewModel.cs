using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadCustomConfigurationViewModel : MonoBehaviour
{
	private string applicationPath;

	private string ApplicationPath { get { return applicationPath; } set { applicationPath = value; } }

	private ConfigurationDTO configuration;

	void Start ()
	{
		ApplicationPath = Application.persistentDataPath;
	}


	public void RefreshList ()
	{
		ConfigFilesScrollList dropDownConfigFilesList = GameObject.Find ("GameController").GetComponent<ConfigFilesScrollList> ();

		string[] fileEntries = Directory.GetFiles (ApplicationPath);
		foreach (string fileName in fileEntries) {
			//TODO Refactor this with the new list
			//DropDownListItem listItem = new DropDownListItem (Path.GetFileName (fileName), fileName);
			//dropDownConfigFilesList.AddItem (listItem);
		}
	}

	public void LoadSelectedFile ()
	{
		this.LoadConfigurationDTOfromFile ();
		this.SaveDataToPlayerPrefs ();
	}

	private void LoadConfigurationDTOfromFile ()
	{
		try {
			ConfigFilesScrollList dropDownConfigFilesList = GameObject.Find ("GameController").GetComponent<ConfigFilesScrollList> ();
			//dropDownConfigFilesList.SelectedFile
		
			//Debug.Log ("Loaded config file with path: " + selectedItem.FilePAth);
			//configuration = FileManager.Load (selectedItem.Caption);
			Debug.Log ("Loaded config file: " + configuration.Name);
		} catch (Exception ex) {
			this.ShowMessage ("Error at loading file", ex);
		}
	}


	private void SaveDataToPlayerPrefs ()
	{
		PlayerPrefs.SetString ("ConfigurationName", this.configuration.Name);
		PlayerPrefs.SetFloat ("BlurLevel", this.configuration.BlurLevel);
		PlayerPrefs.SetFloat ("TunnelLevel", this.configuration.TunnelLevel);
		PlayerPrefs.SetInt ("DelayLevel", this.configuration.Delay);
		PlayerPrefs.SetInt ("MotionBlur", this.configuration.MotionBlur);
		PlayerPrefs.SetInt ("RedColorDistortion", this.configuration.RedColor);    
		PlayerPrefs.SetInt ("RandomEffects", this.configuration.Randomness);
	}


	private void ShowMessage (string message, Exception ex)
	{
		Text messagesText = GameObject.Find ("MessagesText").GetComponent<Text> ();
		messagesText.text = message + "\n" + ex.Message;
	}

	public void Delete ()
	{
		try {
			ConfigFilesScrollList dropDownConfigFilesList = GameObject.Find ("GameController").GetComponent<ConfigFilesScrollList> ();
			/*DropDownListItem selectedItem = dropDownConfigFilesList.SelectedItem;
			Debug.Log ("File will be deleted: " + selectedItem.FilePAth);
			FileManager.DeleteFile (selectedItem.Caption);
			dropDownConfigFilesList.RemoveItem (selectedItem); */
		} catch (Exception ex) {
			this.ShowMessage ("Error at deleting file", ex);
		}
	}
}
