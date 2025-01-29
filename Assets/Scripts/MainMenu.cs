using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadScene(int loadScene = 1)
    {
        SceneManager.LoadScene(loadScene);
    }

    public void Quit()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
