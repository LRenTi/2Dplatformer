using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointPlayerPos : MonoBehaviour
{
    private Checkpointmaster GM;
    
    // Sorgt dafür, dass beim Laden vom Level, der Player auf eine richtige Position gebracht wird(Default-Location oder Checkpoint-Location)
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<Checkpointmaster>();
        transform.position = GM.lastCheckPointPos;                                      
    }
}
