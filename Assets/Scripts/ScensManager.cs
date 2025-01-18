using UnityEngine;
using UnityEngine.SceneManagement;

namespace WildBall.Manager
{
    public class ScensManager : MonoBehaviour
    {
        public static void ResetLavel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public static void NextLavel()
        {
            if (SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings - 1) 
            { 
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
            }
            else
            {
                FindObjectOfType<UI_Manager>().EndGame();
            }
        }

        public void LoadLavel(int buildIndex)
        {
            SceneManager.LoadScene(buildIndex);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
				Application.Quit();
#endif
        }
    }
}



