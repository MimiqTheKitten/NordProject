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
        transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);
    }

    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        grounded = Physics.Raycast(transform.position, Vector3.down, jumpRayDistance, ground);
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();

            Invoke(nameof(resetJump), jumpCooldown);
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
}
