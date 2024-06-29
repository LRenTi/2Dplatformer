using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // "SerializeField" --> in Unity die Variablen setzen 
    [SerializeField] private GameObject[] waypoints;            // "Waypoints" sind dafür da, um den Enemy eine Path zum Gehen zu geben
    private int currentWaypointIndex = 0;
    [SerializeField] private float speed = 2f;

    [SerializeField] private float jumpheigth;
    [SerializeField] private float jumpLength;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private AudioSource Enemywalksound;
    [SerializeField] private AudioSource Deathsound;

    private Collider2D coll;
    private Rigidbody2D rb;
    private Animator anim;
    private bool facingRight = true;
    private bool dead = false;
    private bool isVisible = false; // Variable um die Sichtbarkeit zu tracken
    
    // Wenn der Player auf den Enemy draufspringt
    public void setDead(bool choice)
    {
        dead = choice;
    }

    void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        transform.localScale = new Vector2(-1, 1);
    }

    private void Update()
    {   
        // Wenn der Enemy stirbt
        if (dead)
        {
            Deathsound.Play();
            anim.SetTrigger("death");
            rb.bodyType = RigidbodyType2D.Static;
            Destroy(gameObject, 0.5f);
            dead = false;
        }
        else
        {   
            // Wenn die Distanz zu einem Waypoint kleiner als 0.1 ist
            if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
            {
                // Für EnemyAnimation und Bewegrichtung
                if (facingRight)
                {
                    transform.localScale = new Vector2(1, 1);
                    facingRight = false;
                }
                else
                {
                    transform.localScale = new Vector2(-1, 1);
                    facingRight = true;
                }

                // Damit sich der Path zu den Waypoints ändert
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;
                }
            }
            
            // Sorgt für die Bewegung
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);

            // Kontrolliert die Sichbarkeit zum Player und spielt/stoppt den Sound dementsprechend --> MUSS in "Update" sein, damit der Sound kontinuierlich abspielt
            if (isVisible && !Enemywalksound.isPlaying)
            {
                Enemywalksound.Play();
            }
            else if (!isVisible && Enemywalksound.isPlaying)
            {
                Enemywalksound.Pause();
            }
        }
    }

    private void OnBecameVisible()
    {
        isVisible = true;
    }

    private void OnBecameInvisible()
    {
        isVisible = false;
    }
}
