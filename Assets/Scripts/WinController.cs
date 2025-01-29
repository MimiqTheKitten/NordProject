using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    GameObject player;
    
    [SerializeField] TMP_Text text;
    public void Lose(GameObject player )
    {
        this.player = player;
        text.text = $"You Won!!";
        Invoke("LoadScene", 1.5f);
    }
    void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}
