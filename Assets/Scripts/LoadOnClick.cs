using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {

	public void LoadScene(int sceneIndex) {
		SceneManager.LoadScene(sceneIndex);
		Debug.Log ("LoadOnClick: Go to scene ->" + sceneIndex);
	}
}	