﻿using System.Collections.Generic;
using Assets.Scripts.DTO;

namespace Assets.Scripts.MetaData
{
	/// <summary>
	/// These are the configurations values proposed by ASN for the corresponding alcohol levels.
	/// The Sober configuration acts as default configuration when none is chosen.
	/// </summary>
	public static class DefaultConfigurations
	{
		public static Configuration Sober = new Configuration {
			Name = ConfigurationNames.Sober,
			BlurLevel = 0,
			TunnelLevel = 0,
			Delay = 0,
			MotionBlur = 0,
			RedColor = 0,
			Randomization = 0
		};

		public static Configuration SlightlyDrunk = new Configuration {
			Name = ConfigurationNames.SlightlyDrunk,
			BlurLevel = 2,
			TunnelLevel = 3,
			Delay = 0,
			MotionBlur = 0,
			RedColor = 0,
			Randomization = 1
		};

		public static Configuration Drunk = new Configuration {
			Name = ConfigurationNames.Drunk,
			BlurLevel = 3,
			TunnelLevel = 5,
			Delay = 0,
			MotionBlur = 0,
			RedColor = 0,
			Randomization = 1
		};

		public static Configuration VeryDrunk = new Configuration {
			Name = ConfigurationNames.VeryDrunk,
			BlurLevel = 5,
			TunnelLevel = 7,
			Delay = 0,
			MotionBlur = 0,
			RedColor = 0,
			Randomization = 1
		};

		public static string CreateNew = ConfigurationNames.CreateNew;
		public static string DeleteExisting = ConfigurationNames.DeleteExisting;

		public static readonly List<string> List = new List<string> {
			ConfigurationNames.Sober,
			ConfigurationNames.SlightlyDrunk,
			ConfigurationNames.Drunk,
			ConfigurationNames.VeryDrunk,
			ConfigurationNames.CreateNew,
			ConfigurationNames.DeleteExisting
		};
	}
}