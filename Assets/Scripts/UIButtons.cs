using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1 Lucas");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
