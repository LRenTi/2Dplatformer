using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Damit der Text funktioniert
using UnityEngine.UI;

public class ItemDisappear : MonoBehaviour
{
    private Animator StrawberryAnim;

    private void Start()
    {
        StrawberryAnim = GetComponent<Animator>();
    }

    //Wenn der Spieler mit einem Objekt dagegen stoßt
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Wenn das Objekt den richtigen Tag hat
        if(collision.gameObject.CompareTag("Player"))
        {
            StrawberryAnim.SetBool("collected", true);
            //Destroy(collision.gameObject);
            //Debug.Log(collision.gameObject);
            //Debug.Log(collision);
        }
    }
}