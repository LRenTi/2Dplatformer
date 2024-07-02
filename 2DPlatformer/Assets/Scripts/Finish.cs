using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;
    private bool levelCompleted = false;
    public GameObject player;

    private Checkpointmaster checkpointMaster; // Referenz zum Checkpointmaster

    private void Start()
    {
        // Befüllt die Variablen
        finishSound = GetComponent<AudioSource>();
        checkpointMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<Checkpointmaster>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Wenn das kollidierte Objekt ein "Player" ist
        if (collision.gameObject.tag == "Player" && !levelCompleted)
        {
            finishSound.Play();
            levelCompleted = true;
            UnlockNewLevel();
            Invoke("CompleteLevel", 2f);
        }
    }

    void UnlockNewLevel()
    {
        // Holt sich den Index vom Level
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            // Speichert den Index in die Levelselection und "unlocked" das nächste Level
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

    private void CompleteLevel()
    {
        // Checkpointposition wird zurückgesetzt auf den Default Wert
        if (checkpointMaster != null)
        {
            checkpointMaster.ResetCheckpoint();
        }

        // Lädt das nächste Level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
