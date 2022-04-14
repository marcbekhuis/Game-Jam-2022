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
    public void Restart()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Level2()
    {
        SceneManager.LoadScene("Scene(Sem)");

    }
}
