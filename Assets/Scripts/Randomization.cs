using System;
using Assets.Scripts.Helpers;
using Assets.Scripts.MetaData;
using Assets.Scripts.MetaData.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System.Globalization;

namespace Assets.Scripts
{
	public class Randomization : MonoBehaviour
	{
        /// <summary>
        /// The interval (seconds) in which the RandomWalk is actually called and the values potentially get altered.
        /// </summary>
        private const int predefinedInterval = 5;
        /// <summary>
        /// The maximum value that is allowed to be reached on the Blur component.
        /// </summary>
        private const float blurValueMax = 10;
        /// <summary>
        /// The maximum value that is allowed to be reached on the TiltShift component.
        /// </summary>
        private const float tunnelValueMax = 15;
        /// <summary>
        /// The minimum value that is allowed to be reached on the Blur and TiltShift component.
        /// </summary>        
		private const float valueMin = 0;

		private float blurLevelCurrent;
		private Randomizer blurRandomizer;
		private long internalTime;
		private float tunnelLevelCurrent;
		private Randomizer tunnelRandomizer;

		private BlurOptimized[] blurComponents;
		private AlcoholTiltShift[] tunnelVisionComponents;

		private Text tunnelValueText;
		private Text blurValueText;
		private Toggle blurToggle;
		private Toggle tunnelToggle;

        /// <summary>
        /// This method initializes the Randomizer components for the Blur and TiltShift effects and saves the all repeatedly accessed components in global variables.
        /// </summary>
		private void Start ()
		{
			internalTime = DateTime.Now.Ticks;
			blurLevelCurrent = PlayerPrefs.GetFloat (PlayerPreferences.BlurLevel);
			blurRandomizer = new Randomizer (blurLevelCurrent, valueMin, blurValueMax);
			tunnelLevelCurrent = PlayerPrefs.GetFloat (PlayerPreferences.TunnelLevel);
			tunnelRandomizer = new Randomizer (tunnelLevelCurrent, valueMin, tunnelValueMax);

			blurComponents = new BlurOptimized[2];
			FillComponents (blurComponents);
			tunnelVisionComponents = new AlcoholTiltShift[2];
			FillComponents (tunnelVisionComponents);

			tunnelValueText = GameObject.Find (SimulatingSubstancesControls.TunnelLevelText).GetComponent<Text> ();
			blurValueText = GameObject.Find (SimulatingSubstancesControls.BlurLevelText).GetComponent<Text> ();
			blurToggle = GameObject.Find (SimulatingSubstancesControls.BlurToggle).GetComponent<Toggle> ();
			tunnelToggle = GameObject.Find (SimulatingSubstancesControls.TunnelToggle).GetComponent<Toggle> ();
		}

        /// <summary>
        /// This method is called on each frame that passes through the ARcamera-component and simply checks whether the Randomization component is active and the defined interval has been reached.
        /// If both of those are true, the RandomWalk is processed for both the Blur and TiltShift component.
        /// </summary>
		private void Update ()
		{
			var timeSpanElapsed = new TimeSpan (DateTime.Now.Ticks - internalTime);
			var hasTimeElapsed = timeSpanElapsed.TotalSeconds >= predefinedInterval;

			if (enabled && hasTimeElapsed) {
				blurLevelCurrent = blurRandomizer.DoRandomWalk (blurLevelCurrent);
				UpdateBlurValue (blurLevelCurrent);
				tunnelLevelCurrent = tunnelRandomizer.DoRandomWalk (tunnelLevelCurrent);
				UpdateTunnelValue (tunnelLevelCurrent);
				blurToggle.isOn = true;
				tunnelToggle.isOn = true;
				internalTime = DateTime.Now.Ticks;
			}
		}

		private void UpdateBlurValue (float newValue)
		{
			blurComponents [0].blurSize = newValue;
			blurComponents [1].blurSize = newValue;
			blurValueText.text = newValue.ToString ("F", CultureInfo.InvariantCulture);
		}

		private void UpdateTunnelValue (float newValue)
		{
			tunnelVisionComponents [0].blurArea = newValue;
			tunnelVisionComponents [1].blurArea = newValue;
			tunnelValueText.text = newValue.ToString ("F", CultureInfo.InvariantCulture);
		}

		private void FillComponents<T> (T[] componentArray)
		{
			componentArray [0] = GameObject.Find (SimulatingSubstancesControls.StereoCameraLeft).GetComponent<T> ();
			componentArray [1] = GameObject.Find (SimulatingSubstancesControls.StereoCameraRight).GetComponent<T> ();
		}
	}
}