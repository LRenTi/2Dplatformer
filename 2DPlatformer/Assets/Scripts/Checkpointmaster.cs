using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpointmaster : MonoBehaviour
{
    private static Checkpointmaster instance;
    public Vector2 lastCheckPointPos = new Vector2(1, 1);

    void Awake()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(instance);
        } else {
           Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {   
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
