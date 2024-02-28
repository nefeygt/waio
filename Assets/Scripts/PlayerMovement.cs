using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D charRigidBody;
    private BoxCollider2D boxCollider;
    private SpriteRenderer sprite;
    private Animator animator;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSoundEffect;
    
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
        dirX = Input.GetAxisRaw("Horizontal");
        charRigidBody.velocity = new Vector2(dirX * movementSpeed, charRigidBody.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            charRigidBody.velocity = new Vector2(charRigidBody.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (charRigidBody.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (charRigidBody.velocity.y < -.1f)
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
