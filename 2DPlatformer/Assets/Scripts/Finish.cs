using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;
    private bool levelCompleted = false;
    public GameObject player;

    private Checkpointmaster checkpointMaster; // Reference to the Checkpointmaster

    // Start is called before the first frame update
    private void Start()
    {
        finishSound = GetComponent<AudioSource>();
        checkpointMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<Checkpointmaster>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

    private void CompleteLevel()
    {
        // Reset the checkpoint position before loading the new level
        if (checkpointMaster != null)
        {
            checkpointMaster.ResetCheckpoint();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
