using UnityEngine;
using System.Collections;

public class ConfigurationDTO : MonoBehaviour
{
	private static ConfigurationDTO instance;

	public float BlurLevel { get; set; }

	public float TunnelLevel { get; set; }

	public float DelayLevel { get; set; }


	public static ConfigurationDTO Instance {
		get {
			return instance ?? (instance = new GameObject ("SingConfigurationDTOleton").AddComponent<ConfigurationDTO> ());
		}
	}






}
