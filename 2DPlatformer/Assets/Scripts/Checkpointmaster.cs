using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpointmaster : MonoBehaviour
{
    private static Checkpointmaster instance;
    public Vector2 defaultSpawnPos = new Vector2(-0.04f, 3.19f); // Default spawn coordinates
    public Vector2 lastCheckPointPos;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            lastCheckPointPos = defaultSpawnPos; // Initialize to default spawn position
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Call this method when a new level is loaded
    public void ResetCheckpoint()
    {
        lastCheckPointPos = defaultSpawnPos;
    }
}
