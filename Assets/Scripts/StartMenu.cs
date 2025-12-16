using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{


    public void PlayGame()
    {
        SceneManager.LoadScene(1); // Loads the scene at index 1 (first gameplay scene)
    }

}