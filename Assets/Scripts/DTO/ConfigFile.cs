using System;

namespace Assets.Scripts.DTO
{
	/// <summary>
	/// Simple abstraction of the configuration's file where the user stores its customs configurations 
	/// </summary>
	[Serializable]
	public class ConfigFile
	{
		public ConfigFile (string fileName)
		{
			FileName = fileName;
		}

		public string FileName { get; set; }
	}
}