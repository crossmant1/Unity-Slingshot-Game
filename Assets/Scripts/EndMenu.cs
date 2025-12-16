using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene(0); // Loads the scene at index 1 (first gameplay scene)
    }
}
