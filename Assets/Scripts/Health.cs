using UnityEngine;

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
            wc.Lose(gameObject);
            gameObject.SetActive(false);
        }
    }
}
