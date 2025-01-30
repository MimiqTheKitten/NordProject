using UnityEngine;

public class BeatupEnemy : MonoBehaviour
{
    [Header("Important")]
    [SerializeField] string currentAction = "approach";
    [SerializeField] float maxViewDistance = 7;
    [SerializeField] float engageDistance = 3;
    [SerializeField] float attackDistance = 0.75f;
    [SerializeField] bool stuned;
    [SerializeField] float stunTimer;
    bool canAttack = true;
    [Header("Stats")]
    [SerializeField] float moveSpeed = 10;
    [SerializeField] int health = 40;
   

    public float horizontalMove = -1;
    Rigidbody rb;
    MoveListDoer moveList;
    [SerializeField] GameObject player;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        moveList = GetComponent<MoveListDoer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stuned)
        {
            switch (currentAction)
            {
                case "approach":
                    Approach();
                    break;
                case "engage":
                    Engage();
                    break;
                case "backoff":
                    BackOff();
                    break;
                default:
                    currentAction = "approach";
                    break;
            }
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
        if (Distance().magnitude <= attackDistance && canAttack)
        {
            canAttack = false;
            Attack();
            Invoke(nameof(AttackSwitch), 0.2f);
        }
        else if(canAttack)
        {
            Movement(0.8f);
        }
    }

    void Attack()
    {
        Debug.Log($"{gameObject.name} has attacked");
        moveList.EnemeyAttack();
    }
    void BackOff()
    {
        Movement(-2f);
        if(Distance().magnitude >= engageDistance)
        {
            canAttack = true;
            currentAction = "engage";
        }
    }

    void Movement(float moveMod = 1)
    {
        transform.Translate(Vector3.right * horizontalMove * moveSpeed * moveMod* Time.deltaTime);
    }
    Vector3 Distance()
    {
        return (transform.position - player.transform.position);
    }
    void AttackSwitch()
    {
        currentAction = "backoff";
    }
    void Damage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            gameObject.SetActive(false);
            GameObject.Find("next level").SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)    
    {
        if(other.gameObject.tag == "Hitbox")
        {
            HitBox hitBox = other.gameObject.GetComponent<HitBox>();
            if(hitBox.master == gameObject)
            {
                return;
            }            
            if (hitBox.hasntHit)
            {
                Debug.Log($"Hit by {hitBox.master.name} !!!!!!!!!!");
                stuned = true;
                hitBox.hasntHit = false;
                Damage(hitBox.damage);
                stunTimer = hitBox.damage/10;
                Invoke(nameof(Unstun), stunTimer);
            }
        }
    }
    void Unstun()
    {
        stuned = false;
    }
}
