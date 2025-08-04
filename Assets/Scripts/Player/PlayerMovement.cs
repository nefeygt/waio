using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInputActions playerControls;
    
    private Rigidbody2D charRigidBody;
    private BoxCollider2D boxCollider;
    private SpriteRenderer sprite;
    private Animator animator;

    [SerializeField] private LayerMask jumpableGround;

    //private float dirX = 0f;
    private Vector2 moveDirection = Vector2.zero;
    private InputAction move;
    private InputAction jump;
    
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSoundEffect;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        jump = playerControls.Player.Jump;
        jump.Enable();
        
        jump.performed += ctx => Jump();
    }

    void Jump()
    {
        if (IsGrounded())
        {
            charRigidBody.linearVelocity = new Vector2(charRigidBody.linearVelocity.x, jumpForce);
        }
    }
    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
    }

    // Start is called before the first frame update
    private void Start()
    {
        charRigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        //dirX = Input.GetAxisRaw("Horizontal");
        moveDirection = move.ReadValue<Vector2>();
        
        charRigidBody.linearVelocity = new Vector2(moveDirection.x * movementSpeed, charRigidBody.linearVelocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            charRigidBody.linearVelocity = new Vector2(charRigidBody.linearVelocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (moveDirection.x > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (moveDirection.x < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (charRigidBody.linearVelocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (charRigidBody.linearVelocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        animator.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
