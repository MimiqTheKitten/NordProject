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
    bool canMove = true;
    public float attackCooldown = 1f;
    public float attackDmg = 1f;
    float leftRight;

    public KeyCode attackKey = KeyCode.LeftShift;

    public float playerHight;
    public LayerMask ground;
    public bool grounded;

    float horizontalInput;

    Rigidbody rb;
    CharacterController cc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
        if (canMove)
        {
            transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);
        }
    }

    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (!(horizontalInput == 0))
        {
            leftRight = horizontalInput; //-1 is left, 1 is right
        }
        grounded = Grounded();
        if (Input.GetKey(jumpKey) && canMove && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(resetJump), jumpCooldown);
        }
        if (Input.GetKeyDown(attackKey) && canMove)
        {
            canMove = false;
            AttackLogic();
            Invoke(nameof(AttackCooldown), attackCooldown);
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
            }
            else if(leftRight < 0) 
            {
                Debug.Log("Ground attack left");
            }
        }
        else
        {
            if (leftRight > 0)
            {
                Debug.Log("Air attack right");
            }
            else if (leftRight < 0)
            {
                Debug.Log("Air attack left");
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
