using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 150;
    [SerializeField] WinController wc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [System.Obsolete]
    private void Start()
    {
        wc = FindFirstObjectByType<WinController>();
    }

    public void Damaged(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            string scene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(scene);
            //gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
            Damaged(50);
            Destroy(collision.gameObject);
        }
    }
}
