using System;
using System.Collections;
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
    private Coroutine powerupCoroutine;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpSpeed = 14f;
    //[SerializeField] private float hurtForce = 10f;
    private enum MovementStatus
    {
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

    // Überprüft, ob er am Boden ist(damit er springen kann)
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    private void Update()
    {   
        // Wenn der Player stirbt
        if (hp < 1 && !dead)
        {
            Die();
            dead = true;
        }
        else 
        {
            Movement();
        }

        // Hol den Horizontalen Input vom Spieler
        dirX = Input.GetAxisRaw("Horizontal");
        UpdateAnimationState();
    }

    private void Movement()
    {
        // Horizontal Bewegung
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        // Wenn der Spieler am Boden ist und springt
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

    private void UpdateAnimationState()
    {
        // Spielt die Beweg Sounds ab
        if (status == MovementStatus.running && !Walkeffect.isPlaying)
        {
            Walkeffect.Play();
        }
    	
        // if/else Kette sorgt dafür, dass das Spielermodel in die richtige Richtung schaut UND die richtige Animation abspielt
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

        // if/else Kette sorgt dafür, dass die Animation fürs springen und fallen richtig abgespielt werden
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
        // Wenn das Objekt eine "Strawberry" ist --> erhöhe die Leben um 1
        if (other.gameObject.CompareTag("Strawberry"))
        {
            hp++;
            HPText.text = "HP: " + hp;
        }
        // Wenn das Objekt eine "PowerUp" ist:
        if (other.gameObject.CompareTag("Powerup"))
        {   
            // --> Erhöhe die Springhöhe
            if (other.gameObject.name.Contains("Pineapple"))
            {
                //Debug.Log("Powerup_Pineapple");
                jumpSpeed = 20f;
                GetComponent<SpriteRenderer>().color = Color.yellow;
            }

            // --> Erhöhe die Geschwindigkeit
            if (other.gameObject.name.Contains("Melon"))
            {
                //Debug.Log("Powerup_Melon");
                moveSpeed = 14f;
                GetComponent<SpriteRenderer>().color = Color.red;
            }

            // Refresht denn PowerUp Timer
            if (powerupCoroutine != null)
            {
                StopCoroutine(powerupCoroutine);
            }
            powerupCoroutine = StartCoroutine(ResetPower());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {   
        // Wenn es eine "Trap" ist --> ein HP weniger
        if (other.gameObject.CompareTag("Trap"))
        {
            hp--;
            HPText.text = "HP: " + hp;
        }
        // Wenn es eine "InstaKillTrap" ist --> HP auf 0 setzten
        if (other.gameObject.CompareTag("InstaKillTrap"))
        {
            hp = 0;
            HPText.text = "HP: " + hp;
        }
        // Wenn es eine "Enemy" ist:
        if (other.gameObject.CompareTag("Enemy"))
        {   
            // Wenn der Spieler auf den Enemy springt --> setzten den Enemy auf "Dead" 
            if (status == MovementStatus.falling)
            {
                other.gameObject.GetComponent<EnemyController>().setDead(true);
                rb.velocity = new Vector2(rb.velocity.x, 10);
            }
            // Wenn der Spieler gegen den Enemy läuft --> ein HP weniger
            else if (other.gameObject.transform.position.x > transform.position.x)
            {
                hp--;
                HPText.text = "HP: " + hp;
            }
        }
    }

    private IEnumerator ResetPower()
    {
        yield return new WaitForSeconds(5);
        if (jumpSpeed > 14f)
        {
            jumpSpeed = 14f;
        }

        if (moveSpeed > 7f)
        {
            moveSpeed = 7f;
        }
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
