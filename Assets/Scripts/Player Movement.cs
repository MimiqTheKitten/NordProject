using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement stats")]
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float jumpForce = 10;
    [SerializeField] bool readyToJump = true;
    [SerializeField] float jumpCooldown = 0.2f;
    [SerializeField] float jumpRayDistance = 10;
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Attack")]
    public float attackCooldown = 1f;
    public float airAttackCooldown= .3f;
    public float attackDmg = 1f;
    float leftRight;
    MoveListDoer moveList;
    public bool canMove = true;

    public KeyCode attackKey = KeyCode.LeftShift;

    [Header("Other")]
    public LayerMask ground;
    public bool grounded;

    float horizontalInput;

    Rigidbody rb;
    CharacterController cc;

    Animator anim_player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
        moveList = GetComponent<MoveListDoer>();
        anim_player = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
            transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);


        Debug.Log(leftRight);
    }

    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (!(horizontalInput == 0))
        {
            if (leftRight < 0 && horizontalInput > 0 && GameObject.Find("Swat").transform.rotation.eulerAngles.y > 260)
            {
                anim_player.SetTrigger("180");
            }
            else if(leftRight > 0 && horizontalInput < 0 && GameObject.Find("Swat").transform.rotation.eulerAngles.y < 95)
            {
                anim_player.SetTrigger("180");
            }
            leftRight = horizontalInput; //-1 is left, 1 is right
            if (!anim_player.GetNextAnimatorStateInfo(0).IsName("turn_180") && !anim_player.GetCurrentAnimatorStateInfo(0).IsName("turn_180"))
            {
                anim_player.SetBool("walking", true);
            }
            else anim_player.SetBool("walking", false);
        }
        else
        {
            anim_player.SetBool("walking", false);
        }
        grounded = Grounded();
        if (Input.GetKey(jumpKey) && canMove && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(resetJump), jumpCooldown);
        }
        if (Input.GetKeyDown(attackKey) && canMove && readyToJump)
        {
            canMove = false;
            AttackLogic();
        }
        
    }
    void Jump()
    {
        Debug.Log("Jump");
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    void resetJump()
    {
        readyToJump = true;
    }
    void AttackLogic()
    {
        if (Grounded())
        {
            if(leftRight > 0)
            {
                Debug.Log("Ground attack right");
                moveList.RightNormal();
                Invoke(nameof(AttackCooldown), attackCooldown);
                anim_player.SetTrigger("attack");
            }
            else if(leftRight < 0) 
            {
                Debug.Log("Ground attack left");
                moveList.LeftNormal();
                Invoke(nameof(AttackCooldown), attackCooldown);
                anim_player.SetTrigger("attack");
            }
        }
        else
        {
            if (leftRight > 0)
            {
                Debug.Log("Air attack right");
                moveList.RightAir();
                Invoke(nameof(AttackCooldown), airAttackCooldown);
            }
            else if (leftRight < 0)
            {
                Debug.Log("Air attack left");
                moveList.LeftAir();
                Invoke(nameof(AttackCooldown), airAttackCooldown);
            }
        }
    }
    void AttackCooldown()
    {
        canMove = true;
    }
    bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, jumpRayDistance, ground);
    }
}
