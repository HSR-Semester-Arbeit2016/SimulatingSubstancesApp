using UnityEngine;
using System.Collections;

[System.Serializable]
public class ConfigurationDTO : MonoBehaviour
{
	private static ConfigurationDTO instance;
	private static string configurationName = "";

	public string Name { get { return configurationName; } set { configurationName = value; } }

	private static float blurLevel = 0.0f;

	/// <summary>
	/// Blur level from 0 to 15
	/// </summary>
	/// <value>The blur level.</value>
	public float BlurLevel { get { return blurLevel; } set { blurLevel = value; } }

	private static float tunnelLevel = 0.0f;

	/// <summary>
	/// Tunnel level from 0 to 25
	/// </summary>
	/// <value>The tunnel level.</value>
	public float TunnelLevel { get { return tunnelLevel; } set { tunnelLevel = value; } }


	private static int delayValue = 0;

	/// <summary>
	/// Boolean wrapper. Gets or sets the delay. 0 = off, 1 = on
	/// </summary>
	/// <value>The delay.</value>
	public int Delay { get { return delayValue; } set { delayValue = value; } }


	private  static int motionBlur = 0;

	/// <summary>
	/// Boolean wrapper. Gets or sets the motion blur. 0 = off, 1 = on.
	/// </summary>
	/// <value>The motion blur.</value>
	public int MotionBlur { get { return motionBlur; } set { motionBlur = value; } }


	private static int redColor = 0;

	/// <summary>
	/// Boolean wrapper. Gets or sets the color of the red. 0 = off, 1 = on.
	/// </summary>
	/// <value>The color of the red.</value>
	public int RedColor { get { return redColor; } set { redColor = value; } }

	private static int randomness = 0;

	/// <summary>
	/// Boolean wrapper. Gets or sets the randomness. 0 = off, 1 = on.
	/// </summary>
	/// <value>The randomness.</value>
	public int Randomness { get { return randomness; } set { randomness = value; } }


	public static ConfigurationDTO Instance {
		get {
			return instance ?? (instance = new GameObject ("SingletonConfigurationDTO").AddComponent<ConfigurationDTO> ());
		}
	}
}
