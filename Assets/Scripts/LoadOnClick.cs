using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
	/// <summary>
	/// Simple class used for navigation betwenn scenes
	/// </summary>
	public class LoadOnClick : MonoBehaviour
	{
		public void LoadScene (int sceneIndex)
		{
			SceneManager.LoadScene (sceneIndex);
		}
	}
}