using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	/// <summary>
	/// Class needed by the ListItemButton Prefab in order to keep the references to itself in the Unity Editor. 
	/// This script allows too the creation of the ListItemButton prefab on the runtime and its used only
	/// in the ConfigFilesScrollList class.
	/// </summary>
	public class ListButton : MonoBehaviour
	{
		public Button button;
		public Text nameLabel;
	}
}