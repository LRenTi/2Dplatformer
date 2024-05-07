using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private Animator anim;
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpSpeed = 14f;
    private enum MovementStatus {
        idle,
        running,
        jumping,
        falling
    }

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource jumpSoundEffect;


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Horzontal movement
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        // Vertical movement
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed); 
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementStatus status;

        if (dirX > 0f)
        {
            status = MovementStatus.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            status = MovementStatus.running;
            sprite.flipX = true;
        }
        else
        {
            status = MovementStatus.idle;
        }

        if (rb.velocity.y > .1f)
        {
            status = MovementStatus.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            status = MovementStatus.falling;
        }

        anim.SetInteger("status", (int)status);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }   
}
