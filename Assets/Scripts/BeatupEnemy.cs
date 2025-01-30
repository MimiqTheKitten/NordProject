using UnityEngine;

public class BeatupEnemy : MonoBehaviour
{
    [Header("Important")]
    [SerializeField] string currentAction = "approach";
    [Header("Stats")]
    [SerializeField] float moveSpeed = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentAction)
        {
            case "approach":
                Approach();
                break;
            case "Engage":
                Engage();
                break;
            case "attack":
                Attack();
                break;
            case "backoff":
                BackOff();
                break;
            default:
                currentAction = "approach";
                break;
        }
    }

    void Approach()
    {

    }

    void Engage()
    {

    }

    void Attack()
    {

    }
    void BackOff()
    {

    }

    void Movement()
    {

    }
}
