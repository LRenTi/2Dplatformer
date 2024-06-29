using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpointmaster : MonoBehaviour
{
    private static Checkpointmaster instance;
    public Vector2 defaultSpawnPos = new Vector2(-0.04f, 3.19f); // Default spawn coordinates
    public Vector2 lastCheckPointPos;

    //Wenn die Objekt für das Level zum ersten Mal geladen werden(wird vor "void Start" aufgerufen)
    void Awake()                                            
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);                    // Sorgt dafür, dass wenn das beim Levelreset(zB.: PlayerDeath) die erreichten Checkpoints sich merkt
            lastCheckPointPos = defaultSpawnPos;            // Initialize to default spawn position
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Methode wird beim Laden von einem neuem Level aufgerufen
    public void ResetCheckpoint()                           
    {
        lastCheckPointPos = defaultSpawnPos;
    }
}
