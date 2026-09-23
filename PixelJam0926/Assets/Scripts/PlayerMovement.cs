using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private float moveSpeed = 5f;

    public InputSystem_Actions playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    //private Animator animator;

    protected override void Awake()
    {
        base.Awake();

        if (Instance != this)
            return;
        
        playerControls = new InputSystem_Actions();
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        if (playerControls != null)
        {
            playerControls.Disable();
        }
    }

    private void OnDestroy()
    {
        if (playerControls != null)
        {
            playerControls.Dispose();
        }
    }

    private void OnEnable()
    {
        if (playerControls != null)
            playerControls.Enable();
    }

    private void Update()
    {
        /*if (animator != null)
        {
            animator.SetBool("IsWalking", false);
            return;
        }*/

        PlayerInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Player.Move.ReadValue<Vector2>();

        if (movement == Vector2.zero)
        {
            //animator.SetBool("IsWalking", false);
            //animator.SetFloat("LastInputX", animator.GetFloat("InputX"));
            //animator.SetFloat("LastInputY", animator.GetFloat("InputY"));

            return;
        }

        //animator.SetBool("IsWalking", true);
        //animator.SetFloat("InputX", movement.x);
        //animator.SetFloat("InputY", movement.y);
    }

    private void Move()
    {
        Vector2 newPosition = rb.position + movement * (moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
    }
}
