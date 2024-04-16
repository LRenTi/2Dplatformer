using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Damit der Text funktioniert
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int strawberryCount = 0;
    [SerializeField] private Text strawberryText;

    //Wenn der Spieler mit einem Objekt dagegen stoßt
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Wenn das Objekt den richtigen Tag hat
        if(collision.gameObject.CompareTag("Strawberry"))
        {
            //Zerstört das Strawberry Objekt nach 0.1 Sekunde(damit eine Animation existieren kann)
            Destroy(collision.gameObject, 0.1f);
            strawberryCount++;
            strawberryText.text = "Strawberries: " + strawberryCount;
        }
    }
}

