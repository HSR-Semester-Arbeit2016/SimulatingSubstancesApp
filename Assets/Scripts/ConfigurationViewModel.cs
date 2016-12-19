using Assets.Scripts.Helpers;
using Assets.Scripts.MetaData;
using Assets.Scripts.MetaData.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Globalization;

namespace Assets.Scripts
{
	/// <summary>
	///The management of the Configuration Scene is divided between the ConfigurationViewModel class and the SaveConfigurationSceneViewModel.
	///The ConfigurationViewModel class controls GUI elements like dropdowns and sliders, obtaining from them the user's input, 
	/// setting its values when the scene is loaded and resetting them if desired.
	/// It also saves this user input to the Unity PlayerPrefs.
	/// </summary>
	public class ConfigurationViewModel : MonoBehaviour
	{
		private Text blurLevelText;
		private Text tunnelLevelText;
		private Slider blurSlider;
		private Slider tunnelSlider;
		private Dropdown delayDropdown;
		private Dropdown colorDropdown;
		private Dropdown motionBlurDropdown;
		private Dropdown randomizationDropdown;

		/// <summary>
		/// Loads all the GUI elements and sets the current Blur and Size values
		/// </summary>
		private void Start ()
		{
			blurLevelText = GameObject.Find (ConfigurationControls.BlurLevelText).GetComponent<Text> ();
			tunnelLevelText = GameObject.Find (ConfigurationControls.TunnelLevelText).GetComponent<Text> ();

			blurSlider = GameObject.Find (ConfigurationControls.BlurSlider).GetComponent<Slider> ();
			tunnelSlider = GameObject.Find (ConfigurationControls.TunnelSlider).GetComponent<Slider> ();

			delayDropdown = GameObject.Find (ConfigurationControls.DelayDropdown).GetComponent<Dropdown> ();
			colorDropdown = GameObject.Find (ConfigurationControls.ColorDropdown).GetComponent<Dropdown> ();
			motionBlurDropdown = GameObject.Find (ConfigurationControls.MotionBlurDropdown).GetComponent<Dropdown> ();
			randomizationDropdown = GameObject.Find (ConfigurationControls.RandomizationDropdown).GetComponent<Dropdown> ();

			SetBlurSize (PlayerPrefs.GetFloat (PlayerPreferences.BlurLevel));
			SetTunnelValue (PlayerPrefs.GetFloat (PlayerPreferences.TunnelLevel));
		}

		public void SetTunnelValue (float newTunnelValue)
		{
			PlayerPrefs.SetFloat (PlayerPreferences.TunnelLevel, newTunnelValue);
			tunnelLevelText.text = newTunnelValue.ToString ("F", CultureInfo.InvariantCulture);
		}


		public void SetBlurSize (float newBlurValue)
		{
			PlayerPrefs.SetFloat (PlayerPreferences.BlurLevel, newBlurValue);
			blurLevelText.text = newBlurValue.ToString ("F", CultureInfo.InvariantCulture);
		}

		public void SetDelay (int value)
		{
			PlayerPrefs.SetInt (PlayerPreferences.DelayLevel, value);
			delayDropdown.value = value;
		}

		public void SetMotionBlur (int value)
		{
			PlayerPrefs.SetInt (PlayerPreferences.MotionBlur, value);
			motionBlurDropdown.value = value;
		}

		/// <summary>
		///     Sets the red color distortion on or off.
		/// </summary>
		/// <param name="value">Red color distortion.</param>
		public void SetRedColorDistortion (int value)
		{
			PlayerPrefs.SetInt (PlayerPreferences.RedColorDistortion, value);
			colorDropdown.value = value;
		}

		/// <summary>
		///     Sets the effect randomization on or off.
		/// </summary>
		/// <param name="value">Randomization value.</param>
		public void SetRandomEffects (int value)
		{
			PlayerPrefs.SetInt (PlayerPreferences.Randomization, value);
			randomizationDropdown.value = value;
		}

		public void Reset ()
		{
			PlayerPrefHelper.ResetPlayerPrefs ();
			ResetTextFields ();
			ResetDropDowns ();
			ResetSliderValues ();
		}

		private void ResetTextFields ()
		{
			blurLevelText.text = blurSlider.minValue.ToString ();
			tunnelLevelText.text = tunnelSlider.minValue.ToString ();
		}

		private void ResetSliderValues ()
		{
			blurSlider.value = blurSlider.minValue;
			tunnelSlider.value = tunnelSlider.minValue;
		}

		private void ResetDropDowns ()
		{
			delayDropdown.value = 0;
			colorDropdown.value = 0;
			motionBlurDropdown.value = 0;
			randomizationDropdown.value = 0;
		}
	}
}