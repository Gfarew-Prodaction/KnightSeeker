using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float JumpForce = 5f;

    [SerializeField] private float coyoteTime = 0.12f;
    [SerializeField] private float jumpBufferTime = 0.12f;
    
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask coinMask;
    
    private Animator animator;
    private Rigidbody2D rigidBody;
    private Vector2 moveInput;

    private float coyoteTimer;
    private float bufferTimer;
    private bool isGrounded;
    
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");
    private static readonly int VelocityYHash = Animator.StringToHash("VelocityY");
    
    private InputPlayerSystem playerInput;
    
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerInput = new InputPlayerSystem();
    }

    private void UpdateAnimations()
    {
        animator.SetFloat(SpeedHash, Mathf.Abs(rigidBody.linearVelocity.x));
        animator.SetBool(IsGroundedHash, isGrounded);
        animator.SetFloat(VelocityYHash, rigidBody.linearVelocity.y);
    }

    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Player.Jump.performed += OnJumpPressed;
    }

    private void OnDisable()
    {
        playerInput.Player.Jump.performed -= OnJumpPressed;
        playerInput.Disable();
    }

    private void Update()
    {
        moveInput = playerInput.Player.Move.ReadValue<Vector2>();

        if (coyoteTimer > 0)
        {
            coyoteTimer -= Time.deltaTime;
        }

        if (bufferTimer > 0)
        {
            bufferTimer -= Time.deltaTime;
        }
        
        Flip();
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        rigidBody.linearVelocity = new Vector2(moveInput.x * moveSpeed, rigidBody.linearVelocity.y);
        
        bool canJump = isGrounded || coyoteTimer > 0f;

        if (bufferTimer > 0f && canJump)
        {
                rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, JumpForce);

                bufferTimer = 0f;
                coyoteTimer = 0f;
        }
    }
    
    private void OnJumpPressed(InputAction.CallbackContext ctx)
    {
        bufferTimer = jumpBufferTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        TrySetGrounded(other, true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (IsGroundLayer(other.gameObject.layer))
        {
            isGrounded = false;
            coyoteTimer = coyoteTime;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        TrySetGrounded(other, true);
    }

    private void TrySetGrounded(Collision2D collision, bool value)
    {
        if(!IsGroundLayer(collision.gameObject.layer)) return;

        foreach (var contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                coyoteTimer = coyoteTime;
                return;
            }
        }
    }

    private bool IsGroundLayer(int layer)
    {
        return (groundMask.value & (1 << layer)) != 0;
    }

    private void Flip()
    {
        if (moveInput.x >= 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput.x <= -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }
}
