using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private Animator anim;
    private int hp = 1;
    private float dirX = 0f;
    private bool dead = false;
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
    [SerializeField] private Text HPText;
    [SerializeField] private AudioSource Deathsoundeffect;


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        IsGrounded();
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    private void Update()
    {
        if(hp < 1 && !dead)
        {
            Die();
            dead = true;
        }
        else if(status != MovementStatus.hurt)
        {
            Movement();
        }

        dirX = Input.GetAxisRaw("Horizontal");
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
        if(status == MovementStatus.running && !Walkeffect.isPlaying)
        {
             Walkeffect.Play();
        }
        
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
            Walkeffect.Stop();
            status = MovementStatus.idle;
        }

        if (rb.velocity.y > .1f)
        {
            Walkeffect.Stop();
            status = MovementStatus.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            Walkeffect.Stop();
            status = MovementStatus.falling;
        }

        anim.SetInteger("status", (int)status);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Strawberry"))
        {
            hp++;
            HPText.text = "HP: " + hp;
        }
        //Wenn das Objekt den richtigen Tag hat
        if (other.gameObject.CompareTag("Powerup"))
        {
            if (other.gameObject.name.Contains("Pineapple"))
            {
                Debug.Log("Powerup_Pineapple");
                jumpSpeed = 20f;
                GetComponent<SpriteRenderer>().color = Color.yellow;
                StartCoroutine(ResetPower());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if(status == MovementStatus.falling){
                other.gameObject.GetComponent<EnemyController>().setDead(true);
            }
        }
        if (other.gameObject.CompareTag("Trap"))
        {
            hp--;
            HPText.text = "HP: " + hp;
        }
        if (other.gameObject.CompareTag("InstaKillTrap"))
        {
            hp = 0;
            HPText.text = "HP: " + hp;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (status == MovementStatus.falling)
            {
                rb.velocity = new Vector2(rb.velocity.x, 10);
            }
            else if (other.gameObject.transform.position.x > transform.position.x)
            {
                hp--;
                HPText.text = "HP: " + hp;
            }
            else
            {
                hp--;
                HPText.text = "HP: " + hp;
            }
        }
    }

    private IEnumerator ResetPower()
    {
        yield return new WaitForSeconds(5);
        jumpSpeed = 14f;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    private void Die()
    {
        Deathsoundeffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
