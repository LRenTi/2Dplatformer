using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Checkpointmaster GM;
    [SerializeField] private AudioSource savesound; 
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            savesound.Play();
            GM.lastCheckPointPos = transform.position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<Checkpointmaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
