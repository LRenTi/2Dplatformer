using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;
    [SerializeField] private float speed = 2f;

    [SerializeField] private float jumpheigth;
    [SerializeField] private float jumpLength;
    [SerializeField] private LayerMask Ground;

    private Collider2D coll;
    private Rigidbody2D rb;
    private Animator anim;
    private bool facingRight = true;

    void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        transform.localScale = new Vector2(-1, 1);
    }
    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
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

            currentWaypointIndex++;
            if(currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
        
    }


}
