using UnityEngine;

public class EndMenu : MonoBehaviour
{
    // Diese Methode wird aufgerufen, wenn das Spiel beendet wird
    public void Quit()
    {
        Debug.Log("Quit game");
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
