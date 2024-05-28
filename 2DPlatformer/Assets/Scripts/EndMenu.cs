using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    public void Quit()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
