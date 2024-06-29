using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Checkpointmaster GM;
    // AudioSource beim "Collecten" vom Checkpoint(muss in Unity definiert werden)
    [SerializeField] private AudioSource savesound;                 

    // Wenn ein Objekt kollidiert 
    void OnTriggerEnter2D(Collider2D other)                         
    {   
        // UND diese Objekt einen "Player" Tag hat
        if(other.CompareTag("Player"))                              
        {
            savesound.Play();
            // Variable "lastCheckPointPos" wird im Controller geändert auf die derzeitige Pos. vom Player
            GM.lastCheckPointPos = transform.position;              
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // "GM" ist der Controller der nicht beim Levelreload gelöscht wird(Hat seinen eigene Unity-Tag)
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<Checkpointmaster>();      
    }
}
