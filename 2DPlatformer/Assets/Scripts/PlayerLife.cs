using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private int hp = 1;
    private bool dead = false;
    private float dirX = 0f;

    private enum state {
        idle,
        running,
        jumping,
        falling
    }

    private state status;

    [SerializeField] private Text HPText;
    [SerializeField] private AudioSource Deathsoundeffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(hp < 1 && !dead)
        {
            Die();
            dead = true;
        }

        dirX = Input.GetAxisRaw("Horizontal");
        UpdateAnimationState();

    }

        private void UpdateAnimationState()
    {

        if (dirX > 0f)
        {
            status = state.running;
        }
        else if (dirX < 0f)
        {
            status = state.running;
        }
        else
        {
            status = state.idle;
        }


        if (rb.velocity.y > .1f)
        {
            status = state.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            status = state.falling;
        }

        anim.SetInteger("status", (int)status);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            hp--;
            HPText.text = "HP: " + hp;
        }
        if (collision.gameObject.CompareTag("InstaKillTrap"))
        {
            hp = 0;
            HPText.text = "HP: " + hp;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (status == state.falling)
            {
                rb.velocity = new Vector2(rb.velocity.x, 10);
            }
            else if (collision.gameObject.transform.position.x > transform.position.x)
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

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        Deathsoundeffect.Play();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Wenn das Objekt den richtigen Tag hat
        if (collision.gameObject.CompareTag("Strawberry"))
        {
            hp++;
            HPText.text = "HP: " + hp;
        }
    }
}
