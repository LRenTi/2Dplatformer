using System;
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
    //[SerializeField] private float hurtForce = 10f;
    private enum MovementStatus {
        idle,
        running,
        jumping,
        falling,
        hurt
    }
    private MovementStatus status = MovementStatus.idle;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource Walkeffect;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

 
        IsGrounded();
    }

    // Update is called once per frame
    private void Update()
    {

        if(status != MovementStatus.hurt)
        {
            Movement();
        }

        UpdateAnimationState();
    }

    private void Movement()
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
    }

    private void UpdateAnimationState()
    {

        if (dirX > 0f)
        {
            status = MovementStatus.running;
            sprite.flipX = false;
            //Walkeffect.Play();
        }
        else if (dirX < 0f)
        {
            status = MovementStatus.running;
            sprite.flipX = true;
            
        }
        else
        {
            status = MovementStatus.idle;
            Walkeffect.Stop();
        }

        if(status == MovementStatus.running)
        {

            if (!Walkeffect.isPlaying) { Walkeffect.Play(); }


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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Wenn das Objekt den richtigen Tag hat
        if (collision.gameObject.CompareTag("Powerup_Pineapple"))
        {
            Debug.Log("Powerup_Pineapple");
            Destroy(collision.gameObject);
            jumpSpeed = 20f;
            GetComponent <SpriteRenderer>().color = Color.yellow;
            StartCoroutine(ResetPower());
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if(status == MovementStatus.falling){
                Destroy(other.gameObject);
            }
        }
    }

    private IEnumerator ResetPower()
    {
        yield return new WaitForSeconds(5);
        jumpSpeed = 14f;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
