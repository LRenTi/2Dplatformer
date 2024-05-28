using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Damit der Text funktioniert
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int strawberryCount = 0;
    [SerializeField] private Text strawberryText;
    [SerializeField] private AudioSource Collectsound;
    [SerializeField] private AudioSource Collectsound2;
    //Wenn der Spieler mit einem Objekt dagegen stoßt
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Wenn das Objekt den richtigen Tag hat
        if(collision.gameObject.CompareTag("Strawberry"))
        {
            //Zerstört das Strawberry Objekt nach 0.3 Sekunde(damit eine Animation existieren kann)
            Collectsound.time = 0.2f;
            Collectsound.Play();
            Destroy(collision.gameObject, 0.3f);
            strawberryCount++;
            strawberryText.text = "Strawberries: " + strawberryCount;
        }
        if(collision.gameObject.CompareTag("Powerup"))
        {
            Collectsound2.Play();
            Destroy(collision.gameObject, 0.3f);
        }
    }
}

