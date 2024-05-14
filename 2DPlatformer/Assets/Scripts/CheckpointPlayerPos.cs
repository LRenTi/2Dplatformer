using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointPlayerPos : MonoBehaviour
{
    private Checkpointmaster GM;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<Checkpointmaster>();
        transform.position = GM.lastCheckPointPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
