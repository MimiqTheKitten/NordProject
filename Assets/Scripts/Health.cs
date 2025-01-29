using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 5;

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "bullet")
        {
            Destroy(other.gameObject);
            Debug.Log("hit by bullet");
        }
    }
}
