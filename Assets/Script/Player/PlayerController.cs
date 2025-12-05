using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 6f;
    public float jumpForce = 7f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkDistance = 0.3f;
    public LayerMask groundLayer;

    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public bool isGrounded;

    // FSM states
    public PlayerState currentState;
    public PlayerIdleState idleState;
    public PlayerRunState runState;
    public PlayerJumpState jumpState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        idleState = new PlayerIdleState(this);
        runState = new PlayerRunState(this);
        jumpState = new PlayerJumpState(this);
    }

    void Start()
    {
        SwitchState(idleState);
    }

    void Update()
    {
        GroundCheck();
        currentState.UpdateState();
    }

    public void SwitchState(PlayerState newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    public void Move(Vector3 direction)
    {
        // Fix: Preserve vertical velocity
        Vector3 horizontalVelocity = new Vector3(direction.x * speed, 0, direction.z * speed);
        rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.z);
    }

    public void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, checkDistance, groundLayer);
    }
}
