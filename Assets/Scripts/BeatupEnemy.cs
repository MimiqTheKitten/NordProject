using UnityEngine;

public class BeatupEnemy : MonoBehaviour
{
    [Header("Important")]
    [SerializeField] string currentAction = "approach";
    [Header("Stats")]
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float maxViewDistance = 7;
    [SerializeField] float engageDistance = 3;
    float horizontalMove = -1;
    Rigidbody rb;
    [SerializeField] GameObject player;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentAction)
        {
            case "approach":
                Approach();
                break;
            case "engage":
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
        if (Distance().magnitude <= maxViewDistance)
        {
            Movement();
        }if(Distance().magnitude <= engageDistance)
        {
            currentAction = "engage";
        }
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
        transform.Translate(Vector3.right * horizontalMove * moveSpeed * Time.deltaTime);
    }
    Vector3 Distance()
    {
        return (transform.position - player.transform.position);
    }
}
