using UnityEngine;

public class HitBox : MonoBehaviour
{
    public int damage = 3;

    public float lifeTime = 4;

    bool hasntHit = true;

    GameObject master;

    public void Setup(GameObject user,int damage,float lifetime, float scaleX = 1, float scaleY = 1)
    {
        //summoner
        master = user;
        gameObject.transform.parent = master.transform;
        this.damage = damage;
        this.lifeTime = lifetime;
        transform.localScale = new Vector3(scaleX, scaleY, 1);
    }


    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.name} and {master.name}");
        if(hasntHit && other.gameObject != master && other.gameObject.GetComponent<Health>() != null)
        {
            hasntHit = false;
            other.gameObject.GetComponent<Health>().Damaged(damage);
            Debug.Log($"doing {damage} damage to {other.name}");
        }
        else
        {
            Debug.Log($"Failed to damage {other.name}");
        }
        
    }
}
