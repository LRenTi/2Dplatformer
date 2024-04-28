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

    [SerializeField] private Text HPText;

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
    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
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
