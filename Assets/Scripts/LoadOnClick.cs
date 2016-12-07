using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class LoadOnClick : MonoBehaviour
    {
        public void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
            Debug.Log("LoadOnClick: Go to scene ->" + sceneIndex);
        }
    }
}