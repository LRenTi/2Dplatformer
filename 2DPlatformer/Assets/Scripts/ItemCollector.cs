using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Damit der Text funktioniert
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int strawberryCount = 0;
    [SerializeField] private Text strawberryText;
    [SerializeField] private AudioSource collectsound_Strawberry;
    [SerializeField] private AudioSource collectsound_PowerUp;
    // Wenn der Spieler mit einem Objekt dagegen stoßt
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        // Wenn das Objekt ein "Strawberry" ist
        if(collision.gameObject.CompareTag("Strawberry"))
        {
            // Zerstört das Strawberry Objekt nach 0.3 Sekunde(damit eine Animation existieren kann)
            Destroy(collision.gameObject, 0.3f);
            
            // Erhöht den Counter UND fügt die Zahl in ein Text hinein(muss in Unity definiert werden) 
            strawberryCount++;
            strawberryText.text = "Strawberries: " + strawberryCount;

            // Spiel den Sound ab
            // MUSS AM ENDE STEHEN, weil wenn der Sound in Unity nicht definiert wurde, stürzt dieses Skript ab. Alles vorher kann aber trotz Absturz funktionieren
            collectsound_Strawberry.time = 0.2f;
            collectsound_Strawberry.Play();
        }
        // Wenn das Objekt ein "PowerUp" ist
        if(collision.gameObject.CompareTag("Powerup"))
        {
            collectsound_PowerUp.Play();
            Destroy(collision.gameObject, 0.3f);
        }
    }
}

